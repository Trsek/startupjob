using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace startupjob.DB
{
    public class AppliciantsStore : _CommonDB
    {
        public new Dictionary<string, SqliteType> columns = new Dictionary<string, SqliteType>()
        {
            { "FirstName", SqliteType.Text },
            { "LastName", SqliteType.Text },
            { "Email", SqliteType.Text },
            { "Phone", SqliteType.Text },
            { "Note", SqliteType.Text },
            { "UrlWeb", SqliteType.Text },
            { "UrlFacebook", SqliteType.Integer },
            { "UrlLinkedIn", SqliteType.Text },
            { "Source_Id", SqliteType.Text },
            { "Status_Id", SqliteType.Integer },
            { "IsDeleted", SqliteType.Integer },
            { "DeletionTime", SqliteType.Integer },
            { "DeleterUserId", SqliteType.Integer },
            { "AnonymizationStart", SqliteType.Integer },
            { "AnonymizationFinish", SqliteType.Integer },
            { "IsPseudonymized", SqliteType.Integer },
            { "IsAnonymized", SqliteType.Integer },
        };

        public AppliciantsStore()
        {
            base.columns = columns;
            base.table = "appliciants";
        }
    }
}
