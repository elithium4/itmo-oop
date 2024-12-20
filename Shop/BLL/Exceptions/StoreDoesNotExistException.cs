﻿namespace BLL.Services.Exceptions
{
    public class StoreDoesNotExistException: Exception
    {
        public StoreDoesNotExistException(int storeId) : base($"Store with id {storeId} does not exist") { }
    }
}
