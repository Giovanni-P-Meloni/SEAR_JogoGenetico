using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSys{

    public static void SaveToDataFrame(int generation, int fitness, float mutationRate){
        string path = Application.persistentDataPath + "/dataframe.df";
        
        using (StreamWriter sw = File.AppendText(path)){
            sw.Write(generation +",");
            sw.Write(fitness +",");
            sw.WriteLine(mutationRate.ToString().Replace(",", "."));
        }
    }
    public static void SaveGeneration(Individual[] population, int generation){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/generation" + generation + ".gen";
        
        FileStream stream = new FileStream(path, FileMode.Create);

        Generation gen =  new Generation(population, generation);

        formatter.Serialize(stream, gen);
        stream.Close();
    }

    public static Generation LoadGeneration(int generation){
        string path = Application.persistentDataPath + "/generation" + generation + ".gen";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Generation gen = formatter.Deserialize(stream) as Generation;
            stream.Close();

            return gen;

        } else{
            Debug.LogError("Generation not found in " + path);
            return null;
        }
    }

    public static void SaveLastBestGen(int lastBestGen){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/lastbestgeneration.gen";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, lastBestGen);

        stream.Close();

    }
    public static int CheckLastBestGen(){
        string path = Application.persistentDataPath + "/lastbestgeneration.gen";

        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int lastBestGen = (int)formatter.Deserialize(stream);
            
            Debug.Log("Last best generation number " + lastBestGen + " found");
            return lastBestGen;
        }else{
            Debug.LogError("There is no last generation");
            return -1;
        }
    }
}
