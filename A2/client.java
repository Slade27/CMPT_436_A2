import java.io.*;
import java.net.*;
import java.util.*;

class client{
    public static void main(String[] Args) {

        Console console = System.console();
        try {

            boolean loop = true;

            Socket s = new Socket("mono", 8080);

            InputStreamReader isr;
            BufferedReader br;
            PrintStream ps;

            isr = new InputStreamReader(s.getInputStream());
            br = new BufferedReader(isr);
            ps = new PrintStream(s.getOutputStream());

            while (loop) {


                System.out.println("OPTIONS");
                System.out.println("1 Create Question");
                System.out.println("2 List Questions");
                System.out.println("3 Read Responses to Question");
                System.out.println("4 Add Comment to Question");
                System.out.println("5 Exit");

                String command = console.readLine();
                 

                if (command.equals("1")) {
                    String question = console.readLine();
                    ps.println("1");
                    ps.println(question);
                    ps.flush();
                }
                
                if (command.equals("2")) {
                    ps.println("2");
                    ps.flush();
                    System.out.println(br.readLine());
                }
                
                if (command.equals("3")) {
                    String question = console.readLine();
                    ps.println("3");
                    ps.println(question);
                    ps.flush();

                    System.out.println(br.readLine());

                }
                
                if (command.equals("4")) {

                    String question = console.readLine();
                    String comment = console.readLine();
                    ps.println("4");
                    ps.println(question);
                    ps.println(comment);
                    ps.flush();
                }
                
                if (command.equals("5")) {
                    loop = false;
                }
            }

            s.close();

        } catch (

        Exception E) {
            System.out.println("oops");
        }
        
    }

}