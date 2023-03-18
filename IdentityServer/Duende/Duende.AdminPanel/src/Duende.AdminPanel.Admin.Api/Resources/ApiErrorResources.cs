// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Duende.AdminPanel.Admin.Api.ExceptionHandling;

namespace Duende.AdminPanel.Admin.Api.Resources
{
  public class ApiErrorResources : IApiErrorResources
  {
    public virtual ApiError CannotSetId()
    {
      return new ApiError
      {
        Code = nameof(CannotSetId),
        Description = ApiErrorResource.CannotSetId
      };
    }
  }
}







