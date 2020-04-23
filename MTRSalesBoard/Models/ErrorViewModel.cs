namespace MTRSalesBoard.Models
{
    public class ErrorViewModel
    {
        //Error viewmodels returns the request when an error is thrown
        #region Properties
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        #endregion
    }
}
