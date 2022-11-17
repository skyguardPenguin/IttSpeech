using AudioListener.Recording;
using AudioListener.Speech;
// See https://aka.ms/new-console-template for more information
//martes 18 istrumento de evaliacion: encuesta, preguntas, google forms.


Recorder Recorder = new Recorder();
bool alive = true;
// void Main()
//{
//    Thread start = new Thread(Start);
//    start.Start();
//}

//void Start()
//{
//    Thread thread = new Thread(Record );
//    ;



//        try
//        {
//            if (alive)
//            {
//                alive = false;
//                thread = new Thread(Record);
//                thread.Start();
//            }
//        }
//        catch(Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }

//}

//void Record()
//{
//    Recorder =  new Recorder(DateTime.Now, DateTime.Now.AddSeconds(5));
//    alive = true;
//}
//Main();

//int resp = 0;
//Recorder recorder = new Recorder();
//Analizer analizer = new Analizer(true) ;
//while(resp != 4)
//{
//    Console.WriteLine("Elija una opción. ");
//    Console.Write("1-Analizar\n");
//    Console.Write("2-Ejecutar comando\n");
//    Console.Write("3-Analizar y ejecutar\n");
//    Console.Write("4-Salir\n");
//    Console.Write("Respuesta: ");
//    string strResp = Console.ReadLine();

//    if (int.TryParse(strResp, out resp) &&resp > 0 && resp < 4)
//        switch (resp)
//        {
//            case 1:
//                string path = Path.Combine(Directory.GetCurrentDirectory(), "Audios");
//                if(!Directory.Exists(path))
//                    Directory.CreateDirectory(path);

//                recorder = new Recorder(DateTime.Now, DateTime.Now.AddSeconds(5),path);

//                Thread tAnalizer = new Thread(analize);
//                tAnalizer.Start();




//                break;
//            case 2:
//                Thread tAnalizer2 = new Thread(analize2);
//                tAnalizer2.Start();
//                break;
//            case 3:
//                break;
//            case 4:
//                break;
//        }
//    else
//        Console.WriteLine("Respuesta inválida, vuelva a intenar, ");

//    Console.Clear();
//}

//void analize()
//{
//    Thread.Sleep(10000);
//    analizer.Analize(recorder.OutputFolder + "\\" + recorder.FileName);

//}void analize2()
//{
//    Thread.Sleep(10000);
//    analizer.Analize("C:\\Users\\sinoa\\source\\repos\\IttSpeech\\ConsoleClient\\bin\\Debug\\net6.0\\Audios\\AudioListener\\audio.wav");

//}