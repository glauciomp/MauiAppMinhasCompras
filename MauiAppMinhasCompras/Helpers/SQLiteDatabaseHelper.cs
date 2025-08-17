using MauiAppMinhasCompras.Models;
/* quando está apagado, cinza claro não estão sendo usado
 retirar depois e fazer a classe
 vamos insetir aqui o using SQLite e o using nome do aplicativo
aqui vamos usar as models*/
using SQLite;
/* SGQLite não é um banco de dados, ele tá mais para uma API. Ele faz
 a trdução do objeto relacional, como um arquivo de texto. Dá para usar
ele como um cache de banco*/

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    /* estamos fazendo uma conexão com o banco de dados SQLite */
    {
        readonly SQLiteAsyncConnection _conn;
        // classe somente leitura - conexão assincrona _conn -
        // então estamos fazendo um armazenamento de conexão
        // com o banco de dados SQLite

        public SQLiteDatabaseHelper(string Path)
        // construtor sempre é chamado quando
        // instanciamos a classe
        // Path é o caminho do banco de dados


        {
            _conn = new SQLiteAsyncConnection(Path);
            // meu campo _conn recebe uma nova conexão
            // Path vai abrir o caminho

            _conn.CreateTableAsync<Produto>().Wait();
            // aqui estamos criando a tabela Produto
            // e o método Wait() é para esperar a criação da tabela
            // se não tiver a tabela, ele cria

        }

        //  public Task<int> Insert(Produto p)
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
                // return + a chamada que tema conxeão
                // insertasync estamos fazendo operação de leitura e escrita
                // em arquivo
        }
        // método para inserir um produto
        // na tabela Produto p
        // p é o meu parametro, minha classe model
        // só consigo fazer insert apartir de um model
        // produto preeenchido
        // insert pode ser void? então vamos testar como int

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?," +
                "Quantidade=?, Preco=? WHERE Id=?";

            // temos que passar os parâmetros abaixo
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Task<int> Delete(int Id)
        {
            // return _conn.DeleteAsync(Id);
            // como saber de onde estamos deletando?

            return _conn.Table<Produto>().DeleteAsync(i => i.Id == Id);
            // acima vamos indicar qual id vamos detelet      
            // você chama a tabela, e lá dentro busca para deletar, 
            // então busca os itens da tabela "i" onde o id do item seja
            // o id que eu passei
        }

        // agora vamos listar os produtos

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
            // quando batemos o ponto depois (). ele mostra
            // os métodos que podemos usar, em forma de lista
            // estamos retornando uma lista de produtos

        }

        public Task<List<Produto>> Search(string q)
        // criamos a estrutura da nossa classe
        // que foi feito colocando todos os public void
        // que serão trocadas por Task
        // acima está definido um parametro q
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
