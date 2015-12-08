using SCS.Dashboard;
using SCS.SissDashboard.DAL;
using System;
using System.Web;
using System.Web.Mvc;

namespace SCS.SsisDashboard.UI.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationLogging logWriter;
        public HomeController()
            : base()
        {
            logWriter = new ApplicationLogging();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetKPI()
        {
            try
            {
                var repo = new KpiRepository();
                var data = repo.Fetch();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                this.LogError(String.Format("{0} List controller method failed with an error of {0}:{1}", "GetKPI", e));
                return Json(new { exception = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetExecutions()
        {
            try
            {
                var repo = new ExecutionRepository();
                var data = repo.Fetch();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                this.LogError(String.Format("{0} List controller method failed with an error of {0}:{1}", "GetExecutions", e));
                return Json(new { exception = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetExecutables(int executionId)
        {
            try
            {
                var repo = new ExecutableRepository();
                var data = repo.Fetch(executionId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                this.LogError(String.Format("{0} List controller method failed with an error of {0}:{1}", "GetExecutables", e));
                return Json(new { exception = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Logging Methods

        #region Debugging Log

        protected void LogInfo(string message)
        {
            if (logWriter != null)
                logWriter.LogInfo(message);
        }

        protected void LogDebug(string message)
        {
            if (logWriter != null)
                logWriter.LogDebug(message);
        }

        protected void LogError(string message)
        {
            if (logWriter != null)
                logWriter.LogError(message);
        }

        protected void LogFatal(string message)
        {
            if (logWriter != null)
                logWriter.LogFatal(message);
        }

        protected void LogWarn(string message)
        {
            if (logWriter != null)
                logWriter.LogWarn(message);
        }

        #endregion

        #endregion

    }
}
