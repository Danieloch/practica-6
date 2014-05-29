using System;
using MySql.Data.MySqlClient;

namespace Practica6
{
	
	public class Conexion
	{
		protected MySqlConnection myConnection;
		public Conexion ()
		{
		}

		protected void abrirConexion(){
			string connectionString =
          		"Server=localhost;" +
	          	"Database=escuela;" +
	          	"User ID=root;" +
	          	"Password=123;" +
	          	"Pooling=false;";
	       this.myConnection = new MySqlConnection(connectionString);
	       this.myConnection.Open();
		}

		protected void cerrarConexion(){
			this.myConnection.Close(); 
			this.myConnection = null;
		}

	}
}