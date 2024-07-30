namespace GerenciamentoApplication.Constants
{
    public static class UriTemplates
    {
        /// <summary>
        /// Routes for tasks
        /// </summary>
        public const string TASK = "/task";
        public const string TASK_COMMENT = "/task/comment";
        public const string TASK_FIND_BY_ID_PROJECT = "/task/idProject";

        /// <summary>
        /// Routes for projects
        /// </summary>
        public const string PROJECT = "/project";

        /// <summary>
        /// Routes for reports
        /// </summary>
        public const string REPORT_LAST_30_DAYS = "/report";
    }
}
