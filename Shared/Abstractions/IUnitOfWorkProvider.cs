namespace Shared.Abstractions;

public interface IUnitOfWorkProvider
{
    IUnitOfWork GetUnitOfWork();
}