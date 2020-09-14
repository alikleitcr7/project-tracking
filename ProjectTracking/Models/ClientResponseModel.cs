using ProjectTracking.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models
{
    public enum ClientResponseStatus
    {
        NoStatus,
        Success,
        Failed
    }

    public class ClientResponseModel<T>
    {
        public T Record { get; set; }
        public ClientResponseStatus Status { get; set; }
        public short StatusCode
        {
            get
            {
                return (short)Status;
            }
        }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public object ExtraData { get; set; }

        public bool IsSuccess
        {
            get
            {
                return Status == ClientResponseStatus.Success || Status == ClientResponseStatus.NoStatus;
            }
        }

        public void SetSuccessStatus(bool success, string message = null)
        {
            Status = success ? ClientResponseStatus.Success : ClientResponseStatus.Failed;

            if (!success)
            {
                Message = message ?? "Something Went Wrong";
            }
            else
            {
                Message = message ?? "Success";
            }
        }

        public void SetInternalException(Exception ex)
        {
            Status = ClientResponseStatus.Failed;
            Message = "Internal Server Error";
            InternalMessage = ex.Message;
        }

        public void SetClientException(ClientException ex)
        {
            Status = ClientResponseStatus.Failed;
            Message = ex.Message;
        }
    }
}
