using Elbek.MContent.Services.Models;

namespace Elbek.MContent.Services.Extensions {
  public static class ResultExtensions {
    public static MContentResult<T> ConvertFromValidationResult<T>(
        this MContentValidationResult validationResult) {
      var result = new MContentResult<T>{
          Errors = validationResult.Errors,
          StatusCode = validationResult.StatusCode, Data = default(T)};

      return result;
    }
  }
}