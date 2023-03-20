using core.Models.DefaultResponses;
using data_porter.Models.DBModels;

namespace data_porter.Models.Models.Upload
{
    /// <summary>
    /// Response
    /// </summary>
    public class UploadResponse : IDefaultResponse
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="azureBlobReference">Azure Blob Reference <see cref="AzureBlobReference"/></param>
        public UploadResponse(AzureBlobReference azureBlobReference)
        {
            AzureBlobReference = azureBlobReference;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="errors">Errors <see cref="List{ErrorInfo}"/></param>
        public UploadResponse(List<ErrorInfo> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="azureBlobReference">Azure Blob Reference <see cref="AzureBlobReference"/></param>
        /// <param name="errors">Errors <see cref="List{ErrorInfo}"/></param>
        public UploadResponse(AzureBlobReference azureBlobReference, List<ErrorInfo> errors)
        {
            AzureBlobReference = azureBlobReference;
            Errors = errors;
        }

        /// <summary>
        /// Azure Blob Reference
        /// </summary>
        public AzureBlobReference AzureBlobReference { get; set; } = null!;

        /// <summary>
        /// Errors
        /// </summary>
        public List<ErrorInfo> Errors { get; set; } = new List<ErrorInfo>();
    }
}
