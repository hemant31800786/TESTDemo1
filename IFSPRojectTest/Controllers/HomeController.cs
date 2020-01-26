using IFSPRojectTest.Persitance.db;
using IFSPRojectTest.Persitance.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.Web.Mvc;

namespace IFSPRojectTest.Controllers
{
    public class HomeController : Controller
    {
        IFSTestDBcontext dbContext = null;


        public ActionResult Index(int page = 1, string sort = "", string sortdir = "asc", string search = "")
        {
            try
            {
                if (ViewBag.RowsPerPage == null)
                    ViewBag.RowsPerPage = 5;
                int pageSize = ViewBag.RowsPerPage;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pageSize) - pageSize;
                search = search != "" ? ViewBag.search : search;
                var objResponse = GetCandidates(search, sort, sortdir, skip, pageSize, out totalRecord);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;
                return View(objResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "IFS Test Coding";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hemant Contact Page";
            return View();
        }

        public ActionResult SelectedCandidates(int page = 1, string sort = "", string sortdir = "asc", string search = "")
        {
            try
            {
                if (ViewBag.RowsPerPage == null)
                    ViewBag.RowsPerPage = 5;
                int pageSize = ViewBag.RowsPerPage;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pageSize) - pageSize;
                search = search != "" ? ViewBag.search : search;
                var objResponse = GetSelectedCandidates(search, sort, sortdir, skip, pageSize, out totalRecord, true);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;
                return View(objResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult RejectedCandidates(int page = 1, string sort = "", string sortdir = "asc", string search = "")
        {
            try
            {
                if (ViewBag.RowsPerPage == null)
                    ViewBag.RowsPerPage = 5;
                int pageSize = ViewBag.RowsPerPage;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pageSize) - pageSize;
                search = search != "" ? ViewBag.search : search;
                var objResponse = GetSelectedCandidates(search, sort, sortdir, skip, pageSize, out totalRecord, false);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;
                return View(objResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult RecuritedCandidates(int page = 1, string sort = "", string sortdir = "asc", string search = "")
        {
            try
            {
                if (ViewBag.RowsPerPage == null)
                    ViewBag.RowsPerPage = 5;
                int pageSize = ViewBag.RowsPerPage;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pageSize) - pageSize;
                search = search != "" ? ViewBag.search : search;
                var objResponse = GetRecuritedCandidates(search, sort, sortdir, skip, pageSize, out totalRecord);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;
                return View(objResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Candidate> GetCandidates(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            dbContext = new IFSTestDBcontext();
            List<Candidate> objCandidate = new List<Candidate>();


            List<Candidate> objRegistredCandidates = new List<Candidate>();
            //Get Data From Local DB
            objRegistredCandidates = dbContext.CandidateMaster.ToList();

            //Get Data From Services
            List<Candidate> objCandidateList = Common.GetCandidateListFromWebServices();

            //Check for Data already Selected or not
            var excludCandidates = objCandidateList.Where(x => objRegistredCandidates.Select(y => y.JID).Contains(x.JID)).ToArray();
            var pageResponse = objCandidateList.Except(excludCandidates).ToArray();
            pageResponse = pageResponse.Except(excludCandidates).ToArray();

            objCandidate.AddRange(pageResponse);

            totalRecord = objCandidate.Count();
            if (pageSize > 0)
            {
                objCandidate = objCandidate.Skip(skip).Take(pageSize).ToList();
            }
            return objCandidate;

        }

        public List<Candidate> GetSelectedCandidates(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord, bool? isAccepted)
        {
            dbContext = new IFSTestDBcontext();
            List<Candidate> objAcceptRejectedCandidates = new List<Candidate>();
            objAcceptRejectedCandidates = dbContext.CandidateMaster.ToList().Where(m => m.isAccepted.Equals(isAccepted)).ToList();

            try
            {
                totalRecord = objAcceptRejectedCandidates.Count();
                if (pageSize > 0)
                {
                    objAcceptRejectedCandidates = objAcceptRejectedCandidates.Skip(skip).Take(pageSize).ToList();
                }
                return objAcceptRejectedCandidates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Candidate> GetRecuritedCandidates(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            dbContext = new IFSTestDBcontext();
            List<Candidate> objRecruitedCandidates = new List<Candidate>();
            objRecruitedCandidates = dbContext.CandidateMaster.ToList().Where(m => m.isRecruited.Equals(true)).ToList();

            try
            {
                totalRecord = objRecruitedCandidates.Count();
                if (pageSize > 0)
                {
                    objRecruitedCandidates = objRecruitedCandidates.Skip(skip).Take(pageSize).ToList();
                }
                return objRecruitedCandidates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Candidate> GetCandidateList()
        {
            //Get Data From Url....
            List<Candidate> objCandidate = null;
            try
            {
                objCandidate = Common.GetCandidateListFromWebServices();

                return objCandidate;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objCandidate = null;
            }

        }

        public Candidate GetCandidateById(string Jid)
        {

            Candidate objCandidate = new Candidate();
            try
            {
                List<Candidate> objCandidateList = Common.GetCandidateListFromWebServices();
                objCandidate = objCandidateList.Where(record => record.JID.Equals(Jid)).FirstOrDefault();

                return objCandidate;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objCandidate = null;
            }






        }

        //not implements.....
        public List<Candidate> GetSeachList(int min = 0, int max = 5)
        {
            dbContext = new IFSTestDBcontext();
            List<Candidate> objCandidate = new List<Candidate>();


            List<Candidate> objRegistredCandidates = new List<Candidate>();
            //Get Data From Local DB
            objRegistredCandidates = dbContext.CandidateMaster.ToList();

            //Get Data From Services
            List<Candidate> objCandidateList = Common.GetCandidateListFromWebServices();

            //Check for Data already Selected or not
            var excludCandidates = objCandidateList.Where(x => objRegistredCandidates.Select(y => y.JID).Contains(x.JID)).ToArray();
            var pageResponse = objCandidateList.Except(excludCandidates).ToArray();
            pageResponse = pageResponse.Except(excludCandidates).Where(p => p.Technology.All(d => d.experianceYears > min && d.experianceYears > max)).ToArray();

            objCandidate.AddRange(pageResponse);


            return objCandidate;

        }







        [HttpGet]
        public JsonResult AcceptRejectCandidtae(string JID, bool isAccept)

        {
            dbContext = new IFSTestDBcontext();
            bool result = false;
            Candidate objCandidate = new Candidate();
            try
            {
                objCandidate = GetCandidateById(JID);
                if (objCandidate != null)
                {
                    objCandidate.isAccepted = isAccept;
                    objCandidate.isRecruited = false;
                    dbContext.CandidateMaster.Add(objCandidate);
                    dbContext.Technology.AddRange(objCandidate.Technology);
                    dbContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RecruitCandidtae(string JID, bool isRecruit)

        {
            dbContext = new IFSTestDBcontext();
            bool result = false;
            Candidate objCandidate = dbContext.CandidateMaster.ToList().Where(m => m.JID.Equals(JID)).FirstOrDefault();
            try
            {
                if (objCandidate != null)
                {
                    objCandidate.isRecruited = isRecruit;
                    dbContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }


    }
}
