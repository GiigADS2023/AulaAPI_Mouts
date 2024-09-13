namespace AulaAPI_Mouts.Interface
{
    internal interface IRepositorio<T, t>
    {
        T Salvar(T item);
        bool Alterar(t id, T item);
        void Excluir(T item);
        IEnumerable<T> Consultar();
        T Consultar(t item);
    }
}
