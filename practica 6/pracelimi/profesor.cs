using System;
using MySql.Data.MySqlClient;

namespace Practica6
{
	
	public class profesor : Conexion
	{
		public string cod, nom,id;

		public profesor()
		{
		}

		public void MostrarDatos(){
			this.abrirConexion();
            MySqlCommand myCommand = new MySqlCommand(this.querySelect(), 
			                                          myConnection);
            MySqlDataReader myReader = myCommand.ExecuteReader();	
	        while (myReader.Read()){
	            string id = myReader["id"].ToString();
	            string codigo = myReader["codigo"].ToString();
	            string nombre = myReader["nombre"].ToString();

	            Console.WriteLine("Id: "      +   id   + 
				                  "\nCódigo: " + codigo +
				                  "\nNombre: " + nombre );
	            Console.WriteLine("----------------");
	       }

            myReader.Close();
			myReader = null;
            myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}

		public void Agregar(string codigo, string nombre){
			this.abrirConexion();
			string sql = "INSERT INTO profesor (codigo, nombre,id) VALUES ('" + codigo + "','" + nombre + "','" + id + "')";
			this.ejecutarComando(sql);
			this.cerrarConexion();
		}

		public void editarRegistro (string id)
		{
			string cod, nom, ans;


			this.abrirConexion ();
			MySqlCommand myCommand = new MySqlCommand (this.queryContId (id), myConnection);
			int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
			myCommand = null;
			this.cerrarConexion();

			this.mostrarPorId (id);
			if (res == 1) {
				Console.WriteLine ("Seguro que desea editar el registro?");
				Console.WriteLine ("1. Si");
				Console.WriteLine ("2. No");
				ans = (Console.ReadLine ());

				if (ans == "1") {


					Console.WriteLine ("Ingrese el nuevo codigo: ");
					cod = Console.ReadLine ();
					Console.WriteLine ("Ingrese el nuevo nombre: ");
					nom = Console.ReadLine ();

					this.abrirConexion();
					string sql = "UPDATE profesor SET codigo='" + cod + 
						"', nombre='" + nom + 
						"' WHERE id='" + id + "' ";
					Console.WriteLine ("SE EDITÓ CON EXITO");
					this.ejecutarComando (sql);
					this.cerrarConexion ();
				}
			}
		}

		private void mostrarPorId (string id1)
		{
			try {
				this.abrirConexion ();
				MySqlCommand myCommand;
				myCommand= new MySqlCommand (this.queryContId (id1), myConnection);
				int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
				myCommand=null;
				myCommand= new MySqlCommand (this.queryId (id1), myConnection);
				MySqlDataReader myReader = myCommand.ExecuteReader ();
				if (res == 1) {
					while(myReader.Read()){
						string id = myReader ["id"].ToString ();
						string nombre = myReader ["codigo"].ToString ();
						string codigo = myReader ["nombre"].ToString ();

						Console.WriteLine("");
						Console.WriteLine ("Id: " + id +
							"\nCódigo: " + nombre +
							"\nNombre: " + codigo 
						);
						Console.WriteLine("");
					};

				} else {
					Console.WriteLine ("No existe el registro");
				}
				myReader.Close ();
				myReader = null;
				myCommand.Dispose ();
				myCommand = null;
				this.cerrarConexion ();

			} catch(Exception ex) {
				Console.WriteLine(ex);
			}
		}


		private void borrarRegistro (string id)
		{
			string ans;
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand (this.queryContId (id), myConnection);
			int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
			myCommand = null;
			this.cerrarConexion();
			this.mostrarPorId(id);

			if (res == 1) {
				Console.WriteLine ("Seguro que desea borrar el registro?");
				Console.WriteLine ("1. Si");
				Console.WriteLine ("2. No");
				ans = (Console.ReadLine ());

				if (ans == "1") {
					this.abrirConexion();
					string sql = "DELETE FROM profesorWHERE id='" + id + "'";
					Console.WriteLine ("SE BORRÓ CON EXITO");
					this.ejecutarComando (sql);
					this.cerrarConexion ();
				}
			} else {
				Console.WriteLine("No existe el registro con ese id");
			}

		}

		
		private int ejecutarComando(string sql){
			MySqlCommand myCommand = new MySqlCommand(sql,this.myConnection);
			int afectadas = myCommand.ExecuteNonQuery();
			myCommand.Dispose();
			myCommand = null;
			return afectadas;
		}

		private string querySelect(){
			return "SELECT * " +
	           	"FROM profesor";
		}

		private string queryContId (string id2){
			return "SELECT Count(*) FROM profesor where (id='" + id2 + "')";
		}

	    private string queryId (string id){
			return "SELECT * FROM profesor where id='"+ id +"' ";
		}

	}
}