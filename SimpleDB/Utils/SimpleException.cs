using SimpleDB.Engine;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SimpleDB.Constants;

namespace SimpleDB
{
    /// <summary>
    /// The main exception for SimpleDB
    /// </summary>
    public class SimpleException : Exception
    {
        #region Errors code

        public const int FILE_NOT_FOUND = 101;
        public const int DATABASE_SHUTDOWN = 102;
        public const int INVALID_DATABASE = 103;
        public const int FILE_SIZE_EXCEEDED = 105;
        public const int COLLECTION_LIMIT_EXCEEDED = 106;
        public const int INDEX_DROP_ID = 108;
        public const int INDEX_DUPLICATE_KEY = 110;
        public const int INVALID_INDEX_KEY = 111;
        public const int INDEX_NOT_FOUND = 112;
        public const int INVALID_DBREF = 113;
        public const int LOCK_TIMEOUT = 120;
        public const int INVALID_COMMAND = 121;
        public const int ALREADY_EXISTS_COLLECTION_NAME = 122;
        public const int ALREADY_OPEN_DATAFILE = 124;
        public const int INVALID_TRANSACTION_STATE = 126;
        public const int INDEX_NAME_LIMIT_EXCEEDED = 128;
        public const int INVALID_INDEX_NAME = 129;
        public const int INVALID_COLLECTION_NAME = 130;
        public const int TEMP_ENGINE_ALREADY_DEFINED = 131;
        public const int INVALID_EXPRESSION_TYPE = 132;
        public const int COLLECTION_NOT_FOUND = 133;
        public const int COLLECTION_ALREADY_EXIST = 134;
        public const int INDEX_ALREADY_EXIST = 135;
        public const int INVALID_UPDATE_FIELD = 136;

        public const int INVALID_FORMAT = 200;
        public const int DOCUMENT_MAX_DEPTH = 201;
        public const int INVALID_CTOR = 202;
        public const int UNEXPECTED_TOKEN = 203;
        public const int INVALID_DATA_TYPE = 204;
        public const int PROPERTY_NOT_MAPPED = 206;
        public const int INVALID_TYPED_NAME = 207;
        public const int PROPERTY_READ_WRITE = 209;
        public const int INITIALSIZE_CRYPTO_NOT_SUPPORTED = 210;
        public const int INVALID_INITIALSIZE = 211;
        public const int INVALID_NULL_CHAR_STRING = 212;
        public const int INVALID_FREE_SPACE_PAGE = 213;

        #endregion

        #region Ctor

        public int ErrorCode { get; private set; }
        public long Position { get; private set; }

        public SimpleException(int code, string message)
            : base(message)
        {
            this.ErrorCode = code;
        }

        internal SimpleException(int code, string message, params object[] args)
            : base(string.Format(message, args))
        {
            this.ErrorCode = code;
        }

        internal SimpleException (int code, Exception inner, string message, params object[] args)
        : base (string.Format (message, args), inner)
        {
            this.ErrorCode = code;
        }

        #endregion

        #region Method Errors

        internal static SimpleException FileNotFound(object fileId)
        {
            return new SimpleException(FILE_NOT_FOUND, "File '{0}' not found.", fileId);
        }

        internal static SimpleException DatabaseShutdown()
        {
            return new SimpleException(DATABASE_SHUTDOWN, "Database is in shutdown process.");
        }

        internal static SimpleException InvalidDatabase()
        {
            return new SimpleException(INVALID_DATABASE, "File is not a valid SimpleDB database format or contains a invalid password.");
        }

        //internal static SimpleException FileSizeExceeded(long limit)
        //{
        //    return new SimpleException(FILE_SIZE_EXCEEDED, "Database size exceeds limit of {0}.", FileHelper.FormatFileSize(limit));
        //}

        internal static SimpleException CollectionLimitExceeded(int limit)
        {
            return new SimpleException(COLLECTION_LIMIT_EXCEEDED, "This database exceeded the maximum limit of collection names size: {0} bytes", limit);
        }

        internal static SimpleException IndexNameLimitExceeded(int limit)
        {
            return new SimpleException(INDEX_NAME_LIMIT_EXCEEDED, "This collection exceeded the maximum limit of indexes names/expression size: {0} bytes", limit);
        }

        internal static SimpleException InvalidIndexName(string name, string collection, string reason)
        {
            return new SimpleException(INVALID_INDEX_NAME, "Invalid index name '{0}' on collection '{1}': {2}", name, collection, reason);
        }

        internal static SimpleException InvalidCollectionName(string name, string reason)
        {
            return new SimpleException(INVALID_COLLECTION_NAME, "Invalid collection name '{0}': {1}", name, reason);
        }

        internal static SimpleException IndexDropId()
        {
            return new SimpleException(INDEX_DROP_ID, "Primary key index '_id' can't be dropped.");
        }

        internal static SimpleException TempEngineAlreadyDefined()
        {
            return new SimpleException(TEMP_ENGINE_ALREADY_DEFINED, "Temporary engine already defined or auto created.");
        }

        internal static SimpleException CollectionNotFound(string key)
        {
            return new SimpleException(COLLECTION_NOT_FOUND, "Collection not found: '{0}'", key);
        }


        internal static SimpleException CollectionAlreadyExist(string key)
        {
            return new SimpleException(COLLECTION_ALREADY_EXIST, "Collection already exist: '{0}'", key);
        }

        internal static SimpleException IndexAlreadyExist(string name)
        {
            return new SimpleException(INDEX_ALREADY_EXIST, "Index name '{0}' already exist with a differnt expression. Try drop index first.", name);
        }

        internal static SimpleException InvalidUpdateField(string field)
        {
            return new SimpleException(INVALID_UPDATE_FIELD, "'{0}' can't be modified in UPDATE command.", field);
        }

        internal static SimpleException IndexDuplicateKey(string field, BsonValue key)
        {
            return new SimpleException(INDEX_DUPLICATE_KEY, "Cannot insert duplicate key in unique index '{0}'. The duplicate value is '{1}'.", field, key);
        }

        internal static SimpleException InvalidIndexKey(string text)
        {
            return new SimpleException(INVALID_INDEX_KEY, text);
        }

        internal static SimpleException IndexNotFound(string name)
        {
            return new SimpleException(INDEX_NOT_FOUND, "Index not found '{0}'.", name);
        }

        internal static SimpleException LockTimeout(string mode, TimeSpan ts)
        {
            return new SimpleException(LOCK_TIMEOUT, "Database lock timeout when entering in {0} mode after {1}", mode, ts.ToString());
        }

        internal static SimpleException LockTimeout(string mode, string collection, TimeSpan ts)
        {
            return new SimpleException(LOCK_TIMEOUT, "Collection '{0}' lock timeout when entering in {1} mode after {2}", collection, mode, ts.ToString());
        }

        internal static SimpleException InvalidCommand(string command)
        {
            return new SimpleException(INVALID_COMMAND, "Command '{0}' is not a valid shell command.", command);
        }

        internal static SimpleException AlreadyExistsCollectionName(string newName)
        {
            return new SimpleException(ALREADY_EXISTS_COLLECTION_NAME, "New collection name '{0}' already exists.", newName);
        }

        internal static SimpleException AlreadyOpenDatafile(string filename)
        {
            return new SimpleException(ALREADY_OPEN_DATAFILE, "Your datafile '{0}' is open in another process.", filename);
        }

        internal static SimpleException InvalidDbRef(string path)
        {
            return new SimpleException(INVALID_DBREF, "Invalid value for DbRef in path '{0}'. Value must be document like {{ $ref: \"?\", $id: ? }}", path);
        }

        internal static SimpleException AlreadyExistsTransaction()
        {
            return new SimpleException(INVALID_TRANSACTION_STATE, "The current thread already contains an open transaction. Use the Commit/Rollback method to release the previous transaction.");
        }

        internal static SimpleException CollectionLockerNotFound(string collection)
        {
            return new SimpleException(INVALID_TRANSACTION_STATE, "Collection locker '{0}' was not found inside dictionary.", collection);
        }

        internal static SimpleException InvalidFormat(string field)
        {
            return new SimpleException(INVALID_FORMAT, "Invalid format: {0}", field);
        }

        internal static SimpleException DocumentMaxDepth(int depth, Type type)
        {
            return new SimpleException(DOCUMENT_MAX_DEPTH, "Document has more than {0} nested documents in '{1}'. Check for circular references (use DbRef).", depth, type == null ? "-" : type.Name);
        }

        internal static SimpleException InvalidCtor(Type type, Exception inner)
        {
            return new SimpleException(INVALID_CTOR, inner, "Failed to create instance for type '{0}' from assembly '{1}'. Checks if the class has a public constructor with no parameters.", type.FullName, type.AssemblyQualifiedName);
        }

        internal static SimpleException UnexpectedToken(Token token, string expected = null)
        {
            var position = (token?.Position - (token?.Value?.Length ?? 0)) ?? 0;
            var str = token?.Type == TokenType.EOF ? "[EOF]" : token?.Value ?? "";
            var exp = expected == null ? "" : $" Expected `{expected}`.";

            return new SimpleException(UNEXPECTED_TOKEN, $"Unexpected token `{str}` in position {position}.{exp}")
            {
                Position = position
            };
        }

        internal static SimpleException UnexpectedToken(string message, Token token)
        {
            var position = (token?.Position - (token?.Value?.Length ?? 0)) ?? 0;

            return new SimpleException(UNEXPECTED_TOKEN, message)
            {
                Position = position
            };
        }

        internal static SimpleException InvalidDataType(string field, BsonValue value)
        {
            return new SimpleException(INVALID_DATA_TYPE, "Invalid BSON data type '{0}' on field '{1}'.", value.Type, field);
        }

        internal static SimpleException PropertyReadWrite(PropertyInfo prop)
        {
            return new SimpleException(PROPERTY_READ_WRITE, "'{0}' property must have public getter and setter.", prop.Name);
        }

        internal static SimpleException PropertyNotMapped(string name)
        {
            return new SimpleException(PROPERTY_NOT_MAPPED, "Property '{0}' was not mapped into BsonDocument.", name);
        }

        internal static SimpleException InvalidTypedName(string type)
        {
            return new SimpleException(INVALID_TYPED_NAME, "Type '{0}' not found in current domain (_type format is 'Type.FullName, AssemblyName').", type);
        }

        internal static SimpleException InitialSizeCryptoNotSupported()
        {
            return new SimpleException(INITIALSIZE_CRYPTO_NOT_SUPPORTED, "Initial Size option is not supported for encrypted datafiles.");
        }

        internal static SimpleException InvalidInitialSize()
        {
            return new SimpleException(INVALID_INITIALSIZE, "Initial Size must be a multiple of page size ({0} bytes).", PAGE_SIZE);
        }

        internal static SimpleException InvalidNullCharInString()
        {
            return new SimpleException(INVALID_NULL_CHAR_STRING, "Invalid null character (\\0) was found in the string");
        }

        internal static SimpleException InvalidFreeSpacePage(uint pageID, int freeBytes, int length)
        {
            return new SimpleException(INVALID_FREE_SPACE_PAGE, $"An operation that would corrupt page {pageID} was prevented. The operation required {length} free bytes, but the page had only {freeBytes} available.");
        }

        #endregion
    }
}