using System;

public class Result<T> {

  public Result(bool DidSucceed, string ErrorMessage = "") {
    this.DidSucceed = DidSucceed;
    this.DidFail = !DidSucceed;
    this.ErrorMessage = ErrorMessage;
  }
  public bool DidSucceed { get; set; }
  public bool DidFail { get; set; }
  public string ErrorMessage { get; set; }
  public T Value { get; private set; }

  public static Result<T> Fail(string ErrorMessage){
    return new Result<T>(false, ErrorMessage);
  }

  public static Result<T> Ok(T Value){
    var result = new Result<T>(true);
    result.Value = Value;
    return result;
  }

}