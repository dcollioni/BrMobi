using System;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using BrMobi.Core;
using BrMobi.Core.Enums;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [AlwaysAuthorize]
    public class BaseController : Controller
    {
        protected User LoggedUser
        {
            get
            {
                return Session["User"] != null ? (User)Session["User"] : null;
            }
            set
            {
                Session["User"] = value;
            }
        }

        protected bool CanEvaluate
        {
            get
            {
                return Session["CanEvaluate"] != null ? (bool)Session["CanEvaluate"] : false;
            }
            set
            {
                Session["CanEvaluate"] = value;
            }
        }

        protected string GetBusMarkerImage()
        {
            return GetMarkerImage(MarkerType.Bus);
        }

        protected string GetRideOfferMarkerImage()
        {
            return GetMarkerImage(MarkerType.RideOffer);
        }

        protected string GetRideRequestMarkerImage()
        {
            return GetMarkerImage(MarkerType.RideRequest);
        }

        protected string GetHelpMarkerImage()
        {
            return GetMarkerImage(MarkerType.Help);
        }

        protected string GetMarkerImage(MarkerType markerType)
        {
            var rawUri = WebConfigurationManager.AppSettings["RawUri"];
            string imagePath;

            switch (markerType)
            {
                case MarkerType.Bus:
                    imagePath = WebConfigurationManager.AppSettings["BusMarkerImagePath"];
                    break;
                case MarkerType.RideOffer:
                    imagePath = WebConfigurationManager.AppSettings["RideOfferMarkerImagePath"];
                    break;
                case MarkerType.RideRequest:
                    imagePath = WebConfigurationManager.AppSettings["RideRequestMarkerImagePath"];
                    break;
                case MarkerType.Help:
                    imagePath = WebConfigurationManager.AppSettings["HelpMarkerImagePath"];
                    break;
                default:
                    imagePath = string.Empty;
                    break;
            }

            var uri = string.Format("{0}{1}", rawUri, imagePath);
            return new Uri(uri).AbsoluteUri;
        }

        protected string GetMarkerInfoTemplate(MarkerType markerType)
        {
            string infoHtmlPath = string.Empty;

            switch (markerType)
            {
                case MarkerType.Bus:
                    infoHtmlPath = WebConfigurationManager.AppSettings["BusInfoHtmlPath"];
                    break;
                case MarkerType.RideOffer:
                    infoHtmlPath = WebConfigurationManager.AppSettings["RideOfferInfoHtmlPath"];
                    break;
                case MarkerType.RideRequest:
                    infoHtmlPath = WebConfigurationManager.AppSettings["RideRequestInfoHtmlPath"];
                    break;
                case MarkerType.Help:
                    infoHtmlPath = WebConfigurationManager.AppSettings["HelpInfoHtmlPath"];
                    break;
            }

            return ReadHtmlFile(string.Concat("~", infoHtmlPath)).ToString();
        }

        protected StringBuilder ReadHtmlFile(string htmlFileNameWithPath)
        {
            var store = new StringBuilder();

            using (var htmlReader = new StreamReader(Server.MapPath(htmlFileNameWithPath)))
            {
                store.Append(htmlReader.ReadToEnd());
            }

            return store;
        }
    }
}