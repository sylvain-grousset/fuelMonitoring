import java.sql.SQLException;

import org.xml.sax.Attributes;
import org.xml.sax.helpers.DefaultHandler;

public class Sax extends DefaultHandler{

    private Stats stat = new Stats();

    private int iteration = 0;
    private boolean isVille = false;
    
   
    public Sax(){
        super();
    }

    public void startDocument(){
        System.out.println("Start of document");
    }

    public void endDocument(){
        try {
			stat.makeStats();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    }

    public void startElement(String uri, String name, String qName, Attributes atts){
    	
    	/*this.isVille = false;
    	if (name.compareTo("ville") == 0) {
    		this.isVille = true;
    	}*/
        if (name.compareTo("prix") == 0){
            if(atts.getValue("nom").compareTo("Gazole") == 0){
                this.stat.setGazole(Float.valueOf(atts.getValue("valeur")));
            }
            if(atts.getValue("nom").compareTo("SP98") == 0){
                this.stat.setSP98(Float.valueOf(atts.getValue("valeur")));
            }
            if(atts.getValue("nom").compareTo("E85") == 0){
                this.stat.setE85(Float.valueOf(atts.getValue("valeur")));
            }
            //System.out.println("Balise ouvrante :" + qName);
        }
    }

    public void endElement(String uri, String name, String qName){
        //System.out.println("Balise fermante :"+ qName);
    }

    public void characters(char ch[], int start, int length){
    	if (this.isVille == true){
    		System.out.println(" "+ new String(ch, start, length));
    	}
        //System.out.println(" "+ new String(ch, start, length));
    }

}
