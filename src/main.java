import org.xml.sax.SAXException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.XMLReaderFactory;

import javax.xml.parsers.ParserConfigurationException;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.channels.Channels;
import java.nio.channels.ReadableByteChannel;
import java.sql.SQLException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;

public class main {

    public static void main(String[] args) throws ParserConfigurationException, SAXException, IOException, SQLException {
    	LocalDate now = LocalDate.now();
    	LocalDate prevDate = now.minusDays(1);
    	
    	String formattedDate = now.format(DateTimeFormatter.ofPattern("yyyyMMdd"));
    	String formattedDate2 = prevDate.format(DateTimeFormatter.ofPattern("yyyyMMdd"));
    	
     	if (checkFile(formattedDate2) == true ){
     		UnzipUtility unzipper = new UnzipUtility();
            try {
                unzipper.unzip("histoFichiers/"+formattedDate2+".zip", "histoFichiers/");
                XMLReader xr = XMLReaderFactory.createXMLReader();
                Sax sax = new Sax();
                xr.setContentHandler(sax);
                xr.parse("histoFichiers/PrixCarburants_quotidien_"+formattedDate2+".xml");
            } catch (Exception ex) {
                // some errors occurred
                ex.printStackTrace();
            }
     	}
    }
    
    /**
     * @Description Vérifie si le fichier à une date donnée est disponible par l'API.
     * @param date
     * @return boolean
     * @throws IOException
     */
    private static boolean checkFile(String date) throws IOException {
    	URL website = new URL("https://donnees.roulez-eco.fr/opendata/jour/"+date);
     	ReadableByteChannel rbc = Channels.newChannel(website.openStream());
     	FileOutputStream fos = new FileOutputStream("histoFichiers/"+date+".zip");
     	fos.getChannel().transferFrom(rbc, 0, Long.MAX_VALUE);
     	
     	HttpURLConnection conn = (HttpURLConnection)website.openConnection();
     	String contentType = conn.getContentType();
     	
     	if(contentType.compareTo("application/zip") == 0) {
     		return true;
     	}else {
     		return false;
     	}
    }
}
