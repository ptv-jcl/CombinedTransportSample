using Ptv.XServer.Controls.Map.Layers.Shapes;
using Ptv.XServer.Controls.Map.Tools;
using Ptv.XServer.Controls.Map.Layers;

using XServers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XServers.XLocate;
using XServers.XRoute;

namespace CombinedTransportSample
{
    public partial class Form1 : Form
    {
        private ShapeLayer ferryLayer = new ShapeLayer("Ferries") { SpatialReferenceId = "PTV_MERCATOR" };
        private ShapeLayer routeLayer = new ShapeLayer("Routes") { SpatialReferenceId = "PTV_MERCATOR" };
        private string token;
        public Form1()
        {
            InitializeComponent();

            token = Properties.Settings.Default.Token;
            // dirty hack to load in authors personal credentials from his local machine for debug testing
            if (string.IsNullOrEmpty(token))
                if (File.Exists(@"d:\xservers source\private.txt"))
                    using (var reader = new StreamReader(@"d:\xservers source\private.txt"))
                        token = reader.ReadLine();

            map.XMapUrl = Properties.Settings.Default.XMap;
            map.XMapCredentials = $"xtok:{token}";
            map.XMapStyle = "silkysand";

            //adding combine transport poi layer
            map.Layers.Add(new Ptv.XServer.Controls.Map.Layers.Untiled.XMapLayer("Transports", map.XMapUrl, "xtok", token)
            {
                Caption = "<span class=\"posthilit\">Combined</span> <span class=\"posthilit\">Transports</span>",
                MaxRequestSize = new System.Windows.Size(2048, 2048),
                MinLevel = 4, // Minimal level (defined by Google), for which Poi objects are shown. 
                CustomXMapLayers = new xserver.Layer[]
                {
                    new xserver.StaticPoiLayer
                    {
                        name = "CombinedTransports",
                        visible = true,
                        category = -1,
                        detailLevel = 0
                    }
                }
            });

            map.Layers.Add(ferryLayer);
            map.Layers.Add(routeLayer);
        }

        private void searchFerryButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ferryLayer.Shapes.Clear();
                routeLayer.Shapes.Clear();

                if (string.IsNullOrEmpty(combinedTransportDepartureTextBox.Text))
                {
                    MessageBox.Show("You must fill in the combined transport departure. Only the arrival is optional");
                    return;
                }
                using (var xlocate = XWSClientFactory.GetXLocateWSClient(Properties.Settings.Default.XLocate,token))
                {
                    //geocode both cities, even if destiantion is left blank, i rather keep call numbers low to avoid extra latency
                    var addresses = new Address[]
                    {
                        new Address() { city = combinedTransportDepartureTextBox.Text },
                        new Address() { city = combinedTransportArrivalTextBox.Text },
                    };
                    var addressResponse = xlocate.findAddresses(addresses, null, null, null, null);
                    // check if we have found a start city else abort
                    if (addressResponse[0].wrappedResultList == null || addressResponse[0].wrappedResultList.Length == 0)
                    {
                        MessageBox.Show("Departure city not found");
                        return;
                    }

                    // search for ferrie near departure, use a max range to prevent huge list
                    var departureLocation = new Location() { coordinate = addressResponse[0].wrappedResultList[0].coordinates };
                    var searchOptions = new ReverseSearchOption[]
                    {
                        new ReverseSearchOption() 
                        {
                            param = ReverseSearchParameter.ENGINE_SEARCHRANGE, 
                            value = Properties.Settings.Default.SeachRange.ToString(), 
                        },
                    };
                    var resultFields = new ResultField[]
                    {
                        ResultField.ISBLOCKEDFORTRUCKS,
                        ResultField.ISBLOCKEDFORHAZARDOUSGOODS,
                        ResultField.ISBLOCKEDFORCOMBUSTIBLEGOODS,
                    };
                    var combinedTransportResponse = xlocate.findCombinedTransportByLocation(departureLocation, searchOptions, null, null);

                    var resultCombinedTransports = combinedTransportResponse.wrappedResultList.ToList();

                    // by default xlocate return all ferrie that have a start or end near the location
                    // we are only using the ones near the start, so filter on max range airline distance
                    resultCombinedTransports = resultCombinedTransports.Where(r =>
                        Geotools.AirLineDistanceCalculator.CalculateUsingMercator(
                            r.start.coordinate.point,
                            addressResponse[0].wrappedResultList[0].coordinates.point)
                        <= Properties.Settings.Default.SeachRange).ToList();

                    // if we have found a destination city, filter on ferries that have destination within max range airline
                    if (addressResponse[1].wrappedResultList != null && addressResponse[1].wrappedResultList.Length != 0)
                    {
                        resultCombinedTransports = resultCombinedTransports.Where(r =>
                            Geotools.AirLineDistanceCalculator.CalculateUsingMercator(
                                r.destination.coordinate.point,
                                addressResponse[1].wrappedResultList[0].coordinates.point)
                            <= Properties.Settings.Default.SeachRange).ToList();
                    }
                    // show the result
                    resultCombinedTransportBindingSource.DataSource = resultCombinedTransports;

                    // paint the results on the map
                    #region visualise
                    foreach (var resultCombinedTransport in resultCombinedTransports)
                    {
                        System.Windows.Point startPoint = new System.Windows.Point(
                            resultCombinedTransport.start.coordinate.point.x,
                            resultCombinedTransport.start.coordinate.point.y
                            );
                        System.Windows.Point endPoint = new System.Windows.Point(
                            resultCombinedTransport.destination.coordinate.point.x,
                            resultCombinedTransport.destination.coordinate.point.y
                            );

                        var line = new Ptv.XServer.Controls.Map.Layers.Shapes.MapPolyline()
                        {
                            MapStrokeThickness = 6,
                            Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                            Points = new System.Windows.Media.PointCollection(),
                            ToolTip = $"{resultCombinedTransport.name}\n{resultCombinedTransport.type}\n{resultCombinedTransport.duration}\n{resultCombinedTransport.distance}\n{resultCombinedTransport.id}",
                        };
                        line.Points.Add(startPoint);
                        line.Points.Add(endPoint);
                        ferryLayer.Shapes.Add(line);

                        var startBall = new Ptv.XServer.Controls.Map.Symbols.Ball()
                        {
                            Height = 15,
                            Width = 15,
                            Color = System.Windows.Media.Colors.Green,
                            ToolTip = $"{resultCombinedTransport.start.country} - {resultCombinedTransport.start.name}",
                        };
                        Ptv.XServer.Controls.Map.Layers.Shapes.ShapeCanvas.SetLocation(startBall, startPoint);
                        ferryLayer.Shapes.Add(startBall);

                        var endBall = new Ptv.XServer.Controls.Map.Symbols.Ball()
                        {
                            Height = 15,
                            Width = 15,
                            Color = System.Windows.Media.Colors.Red,
                            ToolTip = $"{resultCombinedTransport.destination.country} - {resultCombinedTransport.destination.name}",
                        };
                        Ptv.XServer.Controls.Map.Layers.Shapes.ShapeCanvas.SetLocation(endBall, endPoint);
                        ferryLayer.Shapes.Add(endBall);
                    }
                    map.WrappedMap.ZoomToAll();
                    #endregion
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = null;
            }
        }

        private void calcualteRouteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                routeLayer.Shapes.Clear();
                using (var xlocate = XWSClientFactory.GetXLocateWSClient(Properties.Settings.Default.XLocate, token))
                using (var xroute = XWSClientFactory.GetXRouteWSClient(Properties.Settings.Default.XRoute, token))
                {
                    // geocode for the from and to city
                    var addresses = new Address[]
                    {
                        new Address() { city = fromCityTextBox.Text },
                        new Address() { city = toCityTextBox.Text },
                    };
                    var addressResponse = xlocate.findAddresses(addresses, null, null, null, null);
                    // if from not found, abort
                    if (addressResponse[0].wrappedResultList == null || addressResponse[0].wrappedResultList.Length == 0)
                    {
                        MessageBox.Show("From city not found");
                        return;
                    }
                    // if to not found, abort
                    if (addressResponse[1].wrappedResultList == null || addressResponse[1].wrappedResultList.Length == 0)
                    {
                        MessageBox.Show("To city not found");
                        return;
                    }

                    // create waypointdesc for call to xroute
                    var wps = new List<WaypointDesc>();
                    var startWp = new WaypointDesc()
                    {
                        linkType = LinkType.AUTO_LINKING,
                        wrappedCoords = new XServers.XRoute.Point[]
                        {
                            new XServers.XRoute.Point()
                            {
                                point = new XServers.XRoute.PlainPoint()
                                {
                                    x=addressResponse[0].wrappedResultList[0].coordinates.point.x,
                                    y=addressResponse[0].wrappedResultList[0].coordinates.point.y,
                                },
                            },
                        },
                    };
                    wps.Add(startWp);

                    // is there is a row select in the datagrid, take the id to use a a combined transport intermidiate
                    if (combinedTransportDataGridView.SelectedRows.Count > 0)
                    {
                        var combinedWp = new WaypointDesc()
                        {
                            combinedTransportID = (combinedTransportDataGridView.SelectedRows[0].DataBoundItem as ResultCombinedTransport).id,
                            viaType = new ViaType() { viaType = ViaTypeEnum.COMBINED_TRANSPORT, },
                        };
                        wps.Add(combinedWp);
                    }

                    var endWp = new WaypointDesc()
                    {
                        linkType = LinkType.AUTO_LINKING,
                        wrappedCoords = new XServers.XRoute.Point[]
                        {
                            new XServers.XRoute.Point()
                            {
                                point = new XServers.XRoute.PlainPoint()
                                {
                                    x=addressResponse[1].wrappedResultList[0].coordinates.point.x,
                                    y=addressResponse[1].wrappedResultList[0].coordinates.point.y,
                                },
                            },
                        },
                    };
                    wps.Add(endWp);
                    var rlo = new ResultListOptions() { polygon = true };

                    // use 40t profile for truck routing
                    var cc = new XServers.XRoute.CallerContext()
                    {
                        wrappedProperties = new XServers.XRoute.CallerContextProperty[]
                        {
                            new XServers.XRoute.CallerContextProperty() { key = "Profile", value="40T", },
                        },
                    };

                    var route = xroute.calculateRoute(wps.ToArray(), null, null, rlo, cc);

                    // paint the route
                    var line = new Ptv.XServer.Controls.Map.Layers.Shapes.MapPolyline()
                    {
                        MapStrokeThickness = 4,
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Brown),
                        Points = new System.Windows.Media.PointCollection(),
                    };
                    foreach (var plainPoint in route.polygon.lineString.wrappedPoints)
                        line.Points.Add(new System.Windows.Point(plainPoint.x, plainPoint.y));
                    routeLayer.Shapes.Add(line);
                    map.WrappedMap.ZoomToAll();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = null;
            }
        }
    }
}
