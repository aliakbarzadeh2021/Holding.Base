using Castle.Windsor;

namespace Holding.Base.Security.Authorization.Seedwork
{
    internal static class AuthorizeConfig
    {
        /// <summary>
        /// کانتینر کسل وینزور
        /// </summary>
        internal static IWindsorContainer Container { get; set; }

        /// <summary>
        /// استفاده از هدر کلاینت برای سطح دسترسی
        /// </summary>
        internal static bool ClientAuthorizeEnabled { get; set; }

        internal static string AllWebClients
        {
            get
            {
                return "SamimDesktop,TeacherOffice,TeacherPanel,StudentPanel,ParentPanel," +
                       "SupportPanel,ExamBank,QuestionBank,WebSiteMaker,PreRegistration," +
                       "Hasht,Poll,CommunicationNetwork,Consultant,Complex";
            }
        }

    }
}