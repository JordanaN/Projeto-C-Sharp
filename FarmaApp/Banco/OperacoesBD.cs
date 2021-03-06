﻿using System.Collections;
using System.Data.SqlClient;
using System.Data;
using Domain.Classes;
using System.Collections.Generic;
using System;

namespace Banco
{
    public class OperacoesBD : ConexaoDB
    {
        public OperacoesBD() : base()
        {
        }

        //metodo de inserir cliente
        public bool Insert(Cliente cliente)
        {

            try
            {
                var sql = "INSERT INTO CLIENTE (NOME, IDADE, EMAIL, RUA, NUMERO, BAIRRO, CIDADE, ESTADO)" +
                       "VALUES (@NOME, @IDADE, @EMAIL, @RUA, @NUMERO, @BAIRRO, @CIDADE, @ESTADO)";

                SqlCommand objCon = new SqlCommand(sql, base.conectar());
                objCon.Parameters.Add(new SqlParameter("@NOME", cliente.Nome));
                objCon.Parameters.Add(new SqlParameter("@IDADE", cliente.Idade));
                objCon.Parameters.Add(new SqlParameter("@EMAIL", cliente.Email));
                objCon.Parameters.Add(new SqlParameter("@RUA", cliente.Rua));
                objCon.Parameters.Add(new SqlParameter("@NUMERO", cliente.Numero));
                objCon.Parameters.Add(new SqlParameter("@BAIRRO", cliente.Bairro));
                objCon.Parameters.Add(new SqlParameter("@CIDADE", cliente.Cidade));
                objCon.Parameters.Add(new SqlParameter("@ESTADO", cliente.Estado));

                var retorno = objCon.ExecuteNonQuery();
                return Convert.ToBoolean(retorno);

            }
            catch (SqlException)
            {
                return false;
                throw;

            }
            finally
            {
                this.desconectar();
            }
        }

        //metodo de atualizar cliente
        public bool Update(Cliente cliente)
        {
            try
            {
                var sql = "UPDATE CLIENTE SET NOME = @NOME, IDADE = @IDADE, EMAIL = @EMAIL, RUA = @RUA, NUMERO = @NUMERO, BAIRRO = @BAIRRO, CIDADE = @CIDADE, ESTADO = @ESTADO " +
               " WHERE ID_CLIENTE = @ID_CLIENTE";

                SqlCommand objCon = new SqlCommand(sql, base.conectar());
                objCon.Parameters.AddWithValue("@ID_CLIENTE", cliente.id_Cliente);
                objCon.Parameters.AddWithValue("@NOME", cliente.Nome);
                objCon.Parameters.AddWithValue("@IDADE", cliente.Idade);
                objCon.Parameters.AddWithValue("@EMAIL", cliente.Email);
                objCon.Parameters.AddWithValue("@RUA", cliente.Rua);
                objCon.Parameters.AddWithValue("@NUMERO", cliente.Numero);
                objCon.Parameters.AddWithValue("@BAIRRO", cliente.Bairro);
                objCon.Parameters.AddWithValue("@CIDADE", cliente.Cidade);
                objCon.Parameters.AddWithValue("@ESTADO", cliente.Estado);

                var retorno = objCon.ExecuteNonQuery();
                return Convert.ToBoolean(retorno);
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                this.desconectar();
            }
        }

        //metodo de deletar cliente
        public bool Delete(Cliente cliente)
        {
            try
            {
                var sql = "DELETE FROM CLIENTE WHERE ID_CLIENTE = @ID_CLIENTE";
                SqlCommand objCon = new SqlCommand(sql, base.conectar());
                objCon.Parameters.AddWithValue("@ID_CLIENTE", cliente.id_Cliente);
                var retorno = objCon.ExecuteNonQuery();
                return Convert.ToBoolean(retorno);
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                this.desconectar();
            }
        }
        
        //metodo para listar cliente na dataGridView
        public List<Cliente> ListaCliente()
        {
            var sql = "select * from cliente";
            var listaDeCliente = new List<Cliente>();

            try
            {
                SqlCommand cmd = new SqlCommand(sql, base.conectar());
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    listaDeCliente.Add(
                        new Cliente {
                            id_Cliente = Convert.ToInt32(rdr["id_cliente"]),
                            Nome = rdr["Nome"].ToString(),
                            Idade = Convert.ToInt32(rdr["Idade"]),
                            Email = rdr["Email"].ToString(),
                            Rua = rdr["Rua"].ToString(),
                            Numero = Convert.ToInt32(rdr["Numero"]),
                            Bairro = rdr["Bairro"].ToString(),
                            Cidade = rdr["Cidade"].ToString(),
                            Estado = rdr["Estado"].ToString()
                        });
                }
                return listaDeCliente;
            }

            catch (Exception)
            {
                throw;
            }
        }

        //metodo de pesquisa por nome
        public List<Cliente> PesquisaNome(String pesquisar)
        {
            var sql = "select * from cliente where nome like @nome";
            pesquisar = "%" + pesquisar + "%";
            var pesquisaNome = new List<Cliente>();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, base.conectar());
                cmd.Parameters.AddWithValue("@nome", pesquisar);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pesquisaNome.Add(new Cliente
                    {
                        id_Cliente = Convert.ToInt32(rdr["id_cliente"]),
                        Nome = rdr["Nome"].ToString(),
                        Idade = Convert.ToInt32(rdr["Idade"]),
                        Email = rdr["Email"].ToString(),
                        Rua = rdr["Rua"].ToString(),
                        Numero = Convert.ToInt32(rdr["Numero"]),
                        Bairro = rdr["Bairro"].ToString(),
                        Cidade = rdr["Cidade"].ToString(),
                        Estado = rdr["Estado"].ToString()
                    });

                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally

            {

                this.desconectar();
                
            }
            return pesquisaNome;
        }

        //metodo de inserir Usuarios do sistema
        public bool InsertUser(Usuario user)
        {
            try
            {
                var sql = "INSERT INTO USUARIO (NOME, SENHA) VALUES (@NOME, @SENHA)";

                SqlCommand objCon = new SqlCommand(sql, base.conectar());
                objCon.Parameters.Add(new SqlParameter("@NOME", user.Nome));
                objCon.Parameters.Add(new SqlParameter("@SENHA", user.Senha));               

                var retorno = objCon.ExecuteNonQuery();
                return Convert.ToBoolean(retorno);

            }
            catch (SqlException)
            {                
                throw;
            }
            finally
            {
                this.desconectar();
            }
        }
        
    }
}


