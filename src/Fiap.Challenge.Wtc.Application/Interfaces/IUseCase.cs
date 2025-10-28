namespace Fiap.Challenge.Wtc.Application.Interfaces;

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}

public interface IUseCase<TResponse>
{
    Task<TResponse> ExecuteAsync();
}