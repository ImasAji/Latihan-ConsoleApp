using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace latihan_Database
{
    class Program
    {
        static void Main(string[] args)
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("********************************");
                Console.WriteLine("WEARNES EDUCATION CENTER MADIUN");
                Console.WriteLine("Informatika 1 - 2018");
                Console.WriteLine("********************************");
                Console.WriteLine();
                Console.WriteLine("Menu : ");
                Console.WriteLine("1. Data Siswa");
                Console.WriteLine("2. Data Guru");
                Console.WriteLine("3. Data Nilai");
                Console.WriteLine("0. Keluar");
                Console.WriteLine("----------------");
                Console.Write("Masukkan Pilihan Anda [1-0] : ");
                pilihan = int.Parse(Console.ReadLine());

                if (pilihan == 1)
                {
                    int pilihansiswa;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(">>> Pengolahan Data Siswa <<<");
                        Console.WriteLine("=============================");
                        Console.WriteLine("1.Tambah Data Siswa");
                        Console.WriteLine("2.Tampil Data Siswa");
                        Console.WriteLine("3.Edit Data Siswa");
                        Console.WriteLine("4.Hapus Data Siswa");
                        Console.WriteLine("5.Kembali ke Menu Awal");
                        Console.Write("Masukan pilihan anda [1-5] :");
                        pilihansiswa = int.Parse(Console.ReadLine());
                        if (pilihansiswa == 1)
                        {
                            Console.Clear();
                            Console.WriteLine(">>> INPUT DATA SISWA <<<");
                            Console.Write("NIM   :");
                            string nim = Console.ReadLine();
                            Console.Write("NAMA  :");
                            string nama = Console.ReadLine();
                            Console.Write("KELAS :");
                            string kelas = Console.ReadLine();
                            Console.Write("Simpan Data[Y/N]?");
                            Console.Write("Apakah Ingin Menyimpan Data : ");
                            string jawab = Console.ReadLine();
                            if (jawab.ToUpper() == "Y")
                            {
                                try
                                {
                                    string query = "INSERT INTO siswa(nim,nama,kelas) VALUES (@nim,@nama,@kelas)";
                                    string konekstring = "PROVIDER=MICROSOFT.ACE.OLEDB.12.0;Data Source=DatabaseSiswa.accdb;";
                                    OleDbConnection koneksi = new OleDbConnection(konekstring);
                                    koneksi.Open();
                                    OleDbCommand cmd = new OleDbCommand(query, koneksi);
                                    cmd.Parameters.AddWithValue("Nim", nim);
                                    cmd.Parameters.AddWithValue("Nama", nama);
                                    cmd.Parameters.AddWithValue("Kelas", kelas);
                                    cmd.ExecuteNonQuery();
                                    Console.WriteLine();
                                    Console.WriteLine("*** OKE ****");
                                    Console.ReadKey();

                                }
                                catch (OleDbException oleEx)
                                {
                                    Console.WriteLine("Tidak Bisa Masuk, ERROR" + oleEx.Message);
                                    Console.ReadKey();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("**** ERROR ****" + ex.Message);
                                    Console.ReadKey();
                                }
                            }


                        }
                        else if (pilihansiswa == 2)
                        {
                            Console.Clear();
                            Console.WriteLine(">>>Tampil Data Siswa<<<");
                            Console.WriteLine(" Masukan Nama Anda atau kosongi untuk menampilkan semua Data :");
                            string cari = Console.ReadLine();
                            string konekstring = "Provider=Microsoft.Ace.OleDB.12.0;Data source=DatabaseSiswa.accdb";
                            OleDbConnection koneksi = new OleDbConnection(konekstring);
                            koneksi.Open();

                            string query;
                            if (cari == "")
                            {
                                query = "SELECT Nim,Nama,Kelas FROM siswa";
                            }
                            else
                            {
                                query = "SELECT Nim,Nama,Kelas FROM siswa WHERE nama Like'%" + cari + "%'";
                            }


                            OleDbCommand cmd = new OleDbCommand(query, koneksi);
                            OleDbDataReader reader = cmd.ExecuteReader();

                            DataTable siswa = new DataTable();
                            siswa.Load(reader);

                            if (siswa.Rows.Count > 0)
                            {
                                //tampilkan data jika ada data

                                {
                                    Console.WriteLine(" ############################################### ");
                                    Console.WriteLine(" | NIM |           NAMA                 | KELAS |");
                                    Console.WriteLine(" ############################################### ");
                                    foreach (DataRow row in siswa.Rows)
                                    {
                                        Console.WriteLine(" | {0} | {1,-30} | {2,-5} | ",
                                            row["Nim"], row["Nama"], row["kelas"]);
                                    }

                                    Console.WriteLine(" *********************************************** ");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Tidak Ada Data Masuk");
                            }
                            Console.ReadKey();
                        }
                        else if (pilihansiswa == 3)
                        {
                            Console.Clear();
                            Console.WriteLine("===>>> Edit Data Siswa <<<===");
                            Console.WriteLine();
                            Console.Write("Masukkan Nis yang mau di edit : ");
                            string nisLama = Console.ReadLine();

                            string query = "SELECT * FROM siswa WHERE nis=@nis";
                            string koneksiString = "Provider=Microsoft.Ace.OleDB.12.0;Data Source=DatabaseSiswa.accdb";
                            OleDbConnection koneksi = new OleDbConnection(koneksiString);
                            koneksi.Open();

                            OleDbCommand cmd = new OleDbCommand(query, koneksi);
                            cmd.Parameters.AddWithValue("@nis", nisLama);
                            OleDbDataReader reader = cmd.ExecuteReader();

                            DataTable siswa = new DataTable();
                            siswa.Load(reader);

                            if(siswa.Rows.Count == 1)

                                {
                                DataRow row = siswa.Rows[0];
                                Console.WriteLine("Nis   : " + row["nis"]);
                                Console.WriteLine("Nama  : " + row["nama"]);
                                Console.WriteLine("Kelas : " + row["kelas"]);

                                //input data baru
                                Console.WriteLine();
                                Console.Write("Nis Baru   : ");
                                string nisBaru = Console.ReadLine();
                                Console.Write("Nama Baru  : ");
                                string nama = Console.ReadLine();
                                Console.Write("Kelas Baru : ");
                                string kelas = Console.ReadLine();

                                Console.Write("Update Data Siswa : [Y/N] ");
                                string jawab = Console.ReadLine();
                                if (jawab.ToUpper() == "Y")
                                {
                                    query = "UPDATE siswa SET nis=@nisBaru,nama=@nama,kelas=@kelas WHERE nis=@nis";
                                    cmd = new OleDbCommand(query, koneksi);

                                    cmd.Parameters.AddWithValue("@nisBaru", nisBaru);
                                    cmd.Parameters.AddWithValue("@nama", nama);
                                    cmd.Parameters.AddWithValue("@kelas", kelas);
                                    cmd.Parameters.AddWithValue("@nis", nisLama);
                                    cmd.ExecuteNonQuery();
                                    Console.WriteLine();
                                    Console.WriteLine("*** OKE ***");
                                    Console.ReadKey();

                                }
                            }
                            else
                            {
                              Console.WriteLine();
                              Console.WriteLine("*** Salah Memasukan Data Anda ****");
                            }
                        }
                        else if (pilihansiswa == 4)
                        {
                            Console.Clear();
                            Console.WriteLine(">> Hapus Data Siswa");
                            Console.WriteLine();
                            Console.Write("Masukkan Nis yang ingin di Hapus : ");
                            string nis = Console.ReadLine();



                            Console.Write("Yakin bro mau dihapus ? [Y/N] ");
                            string jawab = Console.ReadLine();
                            if (jawab.ToUpper() == "Y")
                            {
                                // --contoh penggunaan procedural
                                string koneksiString = "Provider=Microsoft.Ace.OleDB.12.0;Data Source=DatabaseSiswa.accdb";
                                OleDbConnection koneksi = new OleDbConnection(koneksiString);
                                koneksi.Open();

                                string query = "DELETE FROM TbSiswaa WHERE nis=@nis";
                                OleDbCommand cmd = new OleDbCommand(query, koneksi);
                                cmd.Parameters.AddWithValue("@nis", nis);
                                cmd.ExecuteNonQuery();
                                Console.WriteLine();
                                Console.WriteLine("");
                                Console.ReadKey();
                            }

                        }
                    } while (pilihansiswa != 5);


                }
                else if (pilihan == 2)
                {
                    //pengolahan data guru
                }
                else if (pilihan == 0)
                {

                }
            } while (pilihan != 0);


            //Console.ReadKey();






        }
    }
}



                        


                  