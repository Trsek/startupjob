using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace startupjob.DB
{
    public class DocumentsStore : _CommonDB
    {
        public new Dictionary<string, SqliteType> columns = new Dictionary<string, SqliteType>()
        {
            { "Name", SqliteType.Text },
            { "DocumentType", SqliteType.Integer },
            { "ContentType", SqliteType.Text },
            { "Applicant_Id", SqliteType.Integer },
            { "Guid", SqliteType.Integer },
            { "Size", SqliteType.Integer },
            { "Content_Id", SqliteType.Integer },
            { "CreationTime", SqliteType.Text },
            { "IsDeleted", SqliteType.Integer },
            { "DeletionTime", SqliteType.Integer },
            { "DeleterUserId", SqliteType.Integer },
            { "AssignedToAnything", SqliteType.Integer },
            { "JobAdResponse_Id", SqliteType.Integer },
        };

        public DocumentsStore()
        {
            base.columns = columns;
            base.table = "Documents";
        }
    }
}
