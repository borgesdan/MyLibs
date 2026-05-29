namespace MyAspNetApiLib
{
    /// <summary>
    /// Interface padrão para repositórios.
    /// </summary>
    /// <typeparam name="TEntity">Entidade ou modelo do banco de dados.</typeparam>
    /// <typeparam name="IBaseEntity">Especificação na qual toda entidade deve herdar.</typeparam>
    public interface IRepository<TEntity, IBaseEntity> where TEntity : IBaseEntity
    {
        /// <summary>Obtém um objeto IQueryable para realização de consulta complexas.</summary>
        IQueryable<TEntity> ToQuery();
        /// <summary>Obtém um objeto IQueryable somente leitura.</summary>
        IQueryable<TEntity> ToReadOnlyQuery();

        /// <summary>Adiciona um nova entidade e salva no banco.</summary>
        Task<int> AddAndSaveAsync(TEntity entity);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser atualizada e realiza a operação de salvamento.</summary>
        Task<int> UpdateAndSaveAsync(TEntity entity, bool force);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser deletada e realiza a operação de salvamento.</summary>
        Task<int> DeleteAndSaveAsync(TEntity entity);
        /// <summary>Adiciona vários registros e salva no banco.</summary>
        Task<int> AddRangeAndSaveAsync(IEnumerable<TEntity> list);

        /// <summary>Adiciona uma nova entidade. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.</summary>
        void Add(TEntity entity);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser atualizada. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.</summary>
        void Update(TEntity entity);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser deletada. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.</summary>
        void Delete(TEntity entity);
        /// <summary>Adiciona vários registros. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.o.</summary>
        void AddRange(IEnumerable<TEntity> list);

        /// <summary>Salva as operações no banco de dados.</summary>
        Task<int> SaveChangesAsync();
    }

    /// <summary>
    /// Interface padrão para repositórios.
    /// </summary>
    /// <typeparam name="TEntity">Entidade ou modelo do banco de dados.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>Obtém um objeto IQueryable para realização de consulta complexas.</summary>
        IQueryable<TEntity> ToQuery();
        /// <summary>Obtém um objeto IQueryable somente leitura.</summary>
        IQueryable<TEntity> ToReadOnlyQuery();

        /// <summary>Adiciona um nova entidade e salva no banco.</summary>
        Task<int> AddAndSaveAsync(TEntity entity);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser atualizada e realiza a operação de salvamento.</summary>
        Task<int> UpdateAndSaveAsync(TEntity entity, bool force);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser deletada e realiza a operação de salvamento.</summary>
        Task<int> DeleteAndSaveAsync(TEntity entity);
        /// <summary>Adiciona vários registros e salva no banco.</summary>
        Task<int> AddRangeAndSaveAsync(IEnumerable<TEntity> list);

        /// <summary>Adiciona uma nova entidade. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.</summary>
        void Add(TEntity entity);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser atualizada. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.</summary>
        void Update(TEntity entity);
        /// <summary>Informa que a entidade - que deve ter um Id válido - deve ser deletada. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.</summary>
        void Delete(TEntity entity);
        /// <summary>Adiciona vários registros. SaveChangesAsync() deve ser chamado para realizar a operação de salvar.o.</summary>
        void AddRange(IEnumerable<TEntity> list);

        /// <summary>Salva as operações no banco de dados.</summary>
        Task<int> SaveChangesAsync();
    }
}
