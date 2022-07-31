import java.sql.*;
import java.time.LocalDate;


public class Database {

	 	private static Database instance;
	    private Connection connection;
	    private String url = "jdbc:postgresql://localhost:5432/carburant";
	    private String username = "postgres";
	    private String password = "bpsen";
	
	    
	    private Database() throws SQLException {
	    	try {
	            Class.forName("org.postgresql.Driver");
	            this.connection = DriverManager.getConnection(url, username, password);
	        } catch (ClassNotFoundException ex) {
	            System.out.println("Database Connection Creation Failed : " + ex.getMessage());
	        }
	    }
	    
	    
	    
	public Connection getConnection() {
			return connection;
		}



	public static Database getInstance() throws SQLException {
		if (instance == null) {
            instance = new Database();
        } else if (instance.getConnection().isClosed()) {
            instance = new Database();
        }
		return instance;
	}
	

	public void insert(LocalDate date, float SP98, float E85, float gazole) throws SQLException {
		Statement stmt = this.connection.createStatement();
		String query = "INSERT INTO histo (date, \"SP98\", \"Gazole\") VALUES ('"+date+"',"+SP98+","+gazole+")";
		stmt.executeUpdate(query);
	}

	public void getAll() throws SQLException {
		Statement stmt = this.connection.createStatement();
		ResultSet res = stmt.executeQuery("SELECT * FROM histo");
		
	      while(res.next())
	          System.out.println(res.getString(3));
	}


	/*
			Statement stmt = conn.createStatement();
			ResultSet res = stmt.executeQuery("SELECT * FROM histo");
			
		      while(res.next())
		          System.out.println(res.getString(3));*/
			
	
}
