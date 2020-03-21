namespace Common{
  public class Result<T> {
    public Result(bool DidSucceed, string ErrorMessage = "") {
      this.DidSucceed = DidSucceed;
      this.DidFail = !DidSucceed;
      this.ErrorMessage = ErrorMessage;
    }
    public bool DidSucceed { get; private set; }
    public bool DidFail { get; private set; }
    public string ErrorMessage { get; private set; }
    public T Value { get; internal set; }
  }

  public static class Result {
    public static Result<T> Fail<T>(string ErrorMessage){
      return new Result<T>(false, ErrorMessage);
    }

    public static Result<T> Ok<T>(T Value){
      var result = new Result<T>(true);
      result.Value = Value;
      return result;
    }
  }
}
