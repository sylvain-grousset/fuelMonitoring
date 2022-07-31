import java.sql.SQLException;
import java.time.LocalDate;

public class Stats {

    private float gazole;
    private float SP98;
    private float SP95;
    private float E10;
    private float E85;

    private float iterationGazole;
    private float iterationSP98;
    private float iterationSP95;
    private float iterationE10;
    private float iterationE85;

    public Stats(){
        this.gazole = 0;
        this.SP95 = 0;
        this.SP98 = 0;
        this.iterationGazole = 0;
        this.iterationSP95 = 0;
        this.iterationSP98 = 0;
        this.iterationE10 = 0;

    }

    public void makeStats() throws SQLException{
    	//Database db = Database.getInstance();
    	
    	float moyenneGazole = this.gazole/this.iterationGazole;
    	float moyenne98 = this.SP98/this.iterationSP98;
    	float moyenneE85 = this.E85/this.iterationE85;
    	
    	
    	LocalDate now = LocalDate.now();
    	
    	//db.insert(now, round(moyenne98), round(moyenneE85), round(moyenneGazole));
    	
        System.out.println("Moyenne des prix du gazole :"+round(moyenneGazole));
        System.out.println("Moyenne des prix du SP98 :"+round(moyenne98));
        System.out.println("Moyenne des prix du E85 :"+round(moyenneE85));
    }

    
    private float round(float number) {
    	return (float) (Math.round(number*1000.0)/1000.0);
    }
    
    public void setGazole(float gazole) {
        this.gazole += gazole;
        this.iterationGazole++;
    }

    public void setSP98(float SP98) {
        this.SP98 += SP98;
        this.iterationSP98++;
    }

    public void setSP95(float SP95) {
        this.SP95 += SP95;
        this.iterationSP95++;
    }

    public void setE10(float e10) {
        E10 += e10;
        this.iterationE10++;
    }

    public void setE85(float e85) {
        E85 += e85;
        this.iterationE85++;

    }
}
