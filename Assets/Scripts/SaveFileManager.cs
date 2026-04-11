using UnityEngine;
using System.IO;
using System;
using UnityEditor.Experimental.GraphView;

public class SaveFileManager : MonoBehaviour
{
    /*
     ### 문제 1. 세이브 파일 관리자

        `Application.persistentDataPath` 아래의 세이브 폴더를 탐색하여 저장된 파일 정보를 출력하고, 특정 파일을 복사/삭제할 수 있는 스크립트를 작성하시오.

        **요구사항**

        1. **세이브 폴더 준비**: `SaveData` 폴더를 만들고, `File.WriteAllText`로 테스트용 파일 3개를 생성할 것
           - `save1.txt`, `save2.txt`, `save3.txt` (내용은 자유)
        2. **파일 목록 출력**: `Directory.GetFiles`로 폴더 내 모든 파일을 조회하고, 각 파일의 이름과 확장자를 출력할 것
        3. **파일 복사**: `save1.txt`를 `save1_backup.txt`로 복사할 것 (`File.Copy`)
        4. **파일 삭제**: `save3.txt`를 삭제할 것 (`File.Delete`)
        5. **최종 확인**: 작업 후 파일 목록을 다시 출력하여 결과를 확인할 것

        **예상 출력**

        ```
        === 세이브 파일 목록 ===
        - save1.txt (.txt)
        - save2.txt (.txt)
        - save3.txt (.txt)
        save1.txt → save1_backup.txt 복사 완료
        save3.txt 삭제 완료
        === 작업 후 파일 목록 ===
        - save1.txt (.txt)
        - save1_backup.txt (.txt)
        - save2.txt (.txt)
        ```
     */

    private string savePath;
    private int fileCount = 0;
    private int copyCount = 0;
    private bool isChanged = false;

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "SaveData");
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        else
        {
            ResetDirectory(savePath);
        }
        Debug.Log($"폴더 생성: {savePath}");
    }

    public void OnClickCreateButton()
    {
        fileCount++;
        string fileName = $"save{fileCount}.txt";
        string content = $"{fileCount}번째 세이브 파일입니다.";
        WriteFile(savePath, fileName, content);
        
    }

    public void OnClickButtonRead()
    {
        if (!Directory.Exists(savePath)) { return; }
        
        GetFiles(savePath);
    }

    public void OnClickButtonCopy()
    {
        (string copySrc, string copyResult) = CopyFile(savePath);
        if (!string.IsNullOrEmpty(copyResult) || !string.IsNullOrEmpty(copySrc))
        {
            Debug.Log($"{copySrc} -> {copyResult} 복사 완료");
        }
    }

    public void OnClickButtonDelete()
    {
        if (DeleteFile(savePath, fileCount))
        {
            fileCount--;
        }
        else {Debug.Log("파일을 삭제 실패");}
    }

    public void OnClickButtonReset()
    {
        if (ResetDirectory(savePath))
        {
            Debug.Log($"{savePath}에 모든 파일이 삭제되었습니다.");
        }
        else
        {
            Debug.Log("리셋할 파일이 없습니다.");
        }
        
    }

    private bool ResetDirectory(string path)
    {
        if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0)
        {
            return false;
        }

        Directory.Delete(path, true);
        Directory.CreateDirectory(path);
        isChanged = false;
        fileCount = 0;
        copyCount = 0;

        return true;
    }

    private bool DeleteFile(string path, int count)
    {
        if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0)
        {
            Debug.Log($"{path}에 파일이 존재하지 않습니다.");
            return false;
        }
        string fileToDelete = Path.Combine(path, $"save{count}.txt");
        if (!File.Exists(fileToDelete))
        {
            Debug.Log($"{fileToDelete}이 존재하지 않습니다.");
            return false;
        }
        File.Delete(fileToDelete);
        if (!isChanged) {isChanged = true;}

        Debug.Log($"save{count}.txt 삭제 완료");
        return true;
    }

    private (string, string) CopyFile(string path)
    {
        if (!Directory.Exists(path) && Directory.GetFiles(path).Length == 0)
        {
            Debug.Log($"{path}에 파일이 존재하지 않습니다.");
            return("", "");
        }
        string[] files = Directory.GetFiles(path);

        if (copyCount >= files.Length) 
        {
            Debug.Log($"{copyCount + 1}번째 파일이 존재하지 않아 복사할 수 없습니다.");
            return ("",""); 
        }

        string srcFileName = Path.GetFileName(files[copyCount]);
        string srcExt = Path.GetFileNameWithoutExtension(files[copyCount]);
        string srcPath = files[copyCount];
        string dstFileName = $"{srcExt}_backup.txt";
        string dst = Path.Combine(path, dstFileName);  
          

        File.Copy(srcPath, dst, true); 
        DeleteFile(path, copyCount + 1);
        copyCount++;
        if (!isChanged) { isChanged = true; }

        return (srcFileName, dstFileName);
    }

    private void WriteFile(string path, string fileName, string content)
    {
        if (!Directory.Exists(path))
        {
            Debug.Log($"{path}에 디렉토리가 존재하지 않습니다.");
            return;
        }
        using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        using (StreamWriter writer = new StreamWriter(fs))
        {
            writer.WriteLine(content);
            Debug.Log($"{fileCount}번째 파일이 생성되었습니다.");
        }

    }

    private void GetFiles(string path)
    {
        if (!Directory.Exists(path) && Directory.GetFiles(path).Length == 0)
        {
            Debug.Log($"{path}에 파일이 존재하지 않습니다.");
            return;
        }
        string[] files = Directory.GetFiles(path);
        string[] results = new string[files.Length];   

        for (int i = 0; i < files.Length; i++)
        {
            results[i] = Path.GetFileName(files[i]) ;
        }
        if (!isChanged) { Debug.Log("=== 세이브 파일 목록 ==="); }
        else { Debug.Log("=== 작업 후 파일 목록 ==="); }
        
        for (int i = 0; i < results.Length; i++)
        {
            Debug.Log($"{results[i]} ({Path.GetExtension(results[i])})");   
        }
    }
}
