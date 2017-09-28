using log4net;
using six2015.Models;
using six2015.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace six2015.Controllers
{
    public class HomeController : Controller
    {
        private Model1 _db1 = new Model1();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public enum businessType
        {
            unknown,

            ChangeLicense,//变更户籍姓名
            delay,//延期换证
            lost,//遗失补证
            damage,//损毁换证
            overage,//超龄换证

            expire,//期满换证
            changeaddr,//变更户籍地址
            basicinfo,//基本信息证明
            first,//初领、增加机动车驾驶证自愿业务退办
            network,// 网约车安全驾驶证明

            three,//三年无重大事故证明
            five,//五年安全驾驶证明
            inspectDelay//延期审验
            , bodyDelay//延期提交身体证明
           , changeContact//变更联系方式

            ,inspect,all
        };
        public ActionResult Index()
        {
            return View();
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("NewSum")]
        [HttpGet]
        public async Task<StatisticsResponse> NewSum(businessType bt,string start,string end)
        {
            try
            {
                var startd = DateTime.Now;
                if (string.IsNullOrEmpty(start)||!DateTime.TryParse(start,out startd))
                {
                    return new StatisticsResponse
                    {
                        status = (int)sixerrors.invalidstarttime
                    };
                }
                var endd = DateTime.Now;
                if (string.IsNullOrEmpty(end) || !DateTime.TryParse(end, out endd))
                {
                    return new StatisticsResponse
                    {
                        status = (int)sixerrors.invalidendtime
                    };
                }
                if (bt == businessType.unknown)
                {
                    return new StatisticsResponse
                    {
                        status = (int)sixerrors.businesstypeerror
                    };
                }
                var today = DateTime.Now;
                var yesterday = today.AddDays(-1);
                var todaydb = _db1.COUNT.FirstOrDefault(c => c.TIME.CompareTo(today) <= 0 && c.TIME.CompareTo(yesterday) > 0);
                Log.InfoFormat("today={0}，yesteday={1}", 11, 22);
                if (todaydb == null)
                {
                    todaydb = new COUNT
                    {
                        PAGEVIEW = 0,
                        APPLICATION = 0,
                        PAGEVIEWDAY = 0,
                        APPLICATIONDAY = 0,
                        STARTLEARNINGVOLUME = 0,
                        STARTLEARNINGVOLUMETODAY = 0,
                        KAIFAQU = 0,
                        ZHIFUQU = 0,
                        FUSHANQU = 0,

                        MUPINGQU = 0,
                        LAISHANQU = 0,
                        LONGKOU = 0,
                        ZHAOYUAN = 0,
                        QIXIA = 0,

                        LAIZHOU = 0,
                        CHANGDAO = 0,
                        HAIYANG = 0,
                        LAIYANG = 0,
                        PENGLAI = 0,

                        OTHER = 0,
                        GAOXINQU = 0,
                    };
                }
                var startoftoday = DateTime.Parse(string.Format("{0}-{1}-{2}", today.Year, today.Month, today.Day));
                var todaynum = new statistics
                {
                    PAGEVIEW = todaydb.PAGEVIEWDAY,
                    APPLICATION = todaydb.APPLICATIONDAY,
                    KAIFAQU = todaydb.KAIFAQU,
                    ZHIFUQU = todaydb.ZHIFUQU,
                    FUSHANQU = todaydb.FUSHANQU,

                    MUPINGQU = todaydb.MUPINGQU,
                    LAISHANQU = todaydb.LAISHANQU,
                    LONGKOU = todaydb.LONGKOU,
                    ZHAOYUAN = todaydb.ZHAOYUAN,
                    QIXIA = todaydb.QIXIA,

                    LAIZHOU = todaydb.LAIZHOU,
                    CHANGDAO = todaydb.CHANGDAO,
                    HAIYANG = todaydb.HAIYANG,
                    LAIYANG = todaydb.LAIYANG,
                    PENGLAI = todaydb.PENGLAI,
                    InspectedVolume = _db1.HISTORY.Where(c => c.PROCESSED == "1" && c.OVERTIME.CompareTo(startoftoday) >= 0).Count(),
                    LearningVolume = todaydb.STARTLEARNINGVOLUMETODAY,
                    OTHER = todaydb.OTHER,
                    GAOXINQU = todaydb.GAOXINQU,
                };
                //    var totaldb = _db1.COUNT.Sum(c => c.KAIFAQU);
                var totalnum = new statistics
                {
                    PAGEVIEW = todaydb.PAGEVIEW,
                    APPLICATION = todaydb.APPLICATION,
                    KAIFAQU = _db1.COUNT.Sum(c => c.KAIFAQU),
                    ZHIFUQU = _db1.COUNT.Sum(c => c.ZHIFUQU),
                    FUSHANQU = _db1.COUNT.Sum(c => c.FUSHANQU),

                    MUPINGQU = _db1.COUNT.Sum(c => c.MUPINGQU),
                    LAISHANQU = _db1.COUNT.Sum(c => c.LAISHANQU),
                    LONGKOU = _db1.COUNT.Sum(c => c.LONGKOU),
                    ZHAOYUAN = _db1.COUNT.Sum(c => c.ZHAOYUAN),
                    QIXIA = _db1.COUNT.Sum(c => c.QIXIA),

                    LAIZHOU = _db1.COUNT.Sum(c => c.LAIZHOU),
                    CHANGDAO = _db1.COUNT.Sum(c => c.CHANGDAO),
                    HAIYANG = _db1.COUNT.Sum(c => c.HAIYANG),
                    LAIYANG = _db1.COUNT.Sum(c => c.LAIYANG),
                    PENGLAI = _db1.COUNT.Sum(c => c.PENGLAI),
                    InspectedVolume = _db1.HISTORY.Where(c => c.PROCESSED == "1").Count(),
                    LearningVolume = todaydb.STARTLEARNINGVOLUME,
                    OTHER = _db1.COUNT.Sum(c => c.OTHER),
                    GAOXINQU = _db1.COUNT.Sum(c => c.GAOXINQU),
                };
                return new StatisticsResponse
                {
                    status = 0,
                    today = todaynum,
                    total = totalnum
                };
            }
            catch (Exception ex)
            {
                Log.Error("Statistics,", ex);
                return new StatisticsResponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}