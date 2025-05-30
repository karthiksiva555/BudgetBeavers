namespace BudgetBeavers.Application.Interfaces;

public interface ICrudService<T>: IAddService<T>, IUpdateService<T>, IDeleteService<T>, IGetService<T> where T : class;