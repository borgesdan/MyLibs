namespace MyAspNetApiLib
{
    /// <summary>
    /// Representa um modelo q eu expõe um contexto de banco de dados.
    /// </summary>
    public interface IContextRepository<T> where T : class
    {
        /// <summary>
        /// Obtém o contexto do banco de dados.
        /// </summary>
        T AppDbContext { get; }
    }
}
