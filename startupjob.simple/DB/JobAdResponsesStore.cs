using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace startupjob.DB
{
    public class JobAdResponsesStore : _CommonDB
    {
        public new Dictionary<string, SqliteType> columns = new Dictionary<string, SqliteType>()
        {
            { "FirstName", SqliteType.Text },
            { "LastName", SqliteType.Text },
            { "Note", SqliteType.Text },
            { "Email", SqliteType.Text },
            { "Phone", SqliteType.Text },
            { "CreationTime", SqliteType.Text },
            { "JobAd_Id", SqliteType.Integer },
            { "IsEmailVerified", SqliteType.Integer },
            { "Web", SqliteType.Text },
            { "Source", SqliteType.Text },
        };

        public JobAdResponsesStore()
        {
            base.columns = columns;
            base.table = "JobAdResponses";
        }
    }
}
