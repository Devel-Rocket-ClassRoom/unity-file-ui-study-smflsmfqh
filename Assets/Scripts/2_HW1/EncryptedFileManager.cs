using UnityEngine;
using System.IO;

public class EncryptedFileManager : MonoBehaviour
{
    private string path;
    private const byte key = 0xAB;
    private int size;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "EncryptedData");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
    }

    public void OnClickEncryptedButton()
    {
        string targetFileName = "secret.txt";
        string encryptedFileName = "encrypted.dat";
        string decryptedFileName = "decrypted.txt";

        CreateFile(path, targetFileName);
        EncryptFile(path, targetFileName, encryptedFileName);
        DecryptFile(path, encryptedFileName, decryptedFileName);
        
        bool isSame = CompareFileContent(path, targetFileName, decryptedFileName);
        Debug.Log($"원본과 일치: {isSame}");
    }

    private bool CompareFileContent(string path, string fileName1, string fileName2)
    {
        string prePath = Path.Combine(path, fileName1);
        string newPath = Path.Combine(path, fileName2);
        if (!Directory.Exists(path))
        {
            Debug.Log($"Failed to Compare: {path}가 존재하지 않습니다.");
            return false;
        }

        if (!File.Exists(prePath) || !File.Exists(newPath))
        {
            Debug.Log("Failed to Compare: 파일이 존재하지 않습니다.");
            return false;
        }

        if (File.ReadAllText(prePath) == File.ReadAllText(newPath))
        {
            return true;
        }
        return false;
    }

    private void CreateFile(string path, string fileName)
    {
        if (!Directory.Exists(path))
        {
            Debug.Log($"{path}가 존재하지 않습니다.");
            return;
        }
        string filePath = Path.Combine(path, fileName);

        if (File.Exists(filePath))
        {
            Debug.Log("이미 파일이 존재합니다.");
            return;
        }

        string content = "Hello Unity World";
        File.WriteAllText(filePath, content);
        Debug.Log($"원본: {File.ReadAllText(Path.Combine(path, fileName))}");
    }

    private void EncryptFile(string path, string fileName, string newFileName)
    {
        if (!Directory.Exists(path))
        {
            Debug.Log($"Failed to encrypt: {path}가 존재하지 않습니다.");
            return;
        }
        string filePath = Path.Combine(path, fileName);

        if (!File.Exists(filePath))
        {
            Debug.Log($"Failed to encrypt: {fileName} 파일이 존재하지 않습니다.");
            return;
        }
        string newPath = Path.Combine(path, newFileName);
        using (FileStream fs1 = new FileStream(Path.Combine(path, fileName), FileMode.Open))
        using (FileStream fs2 = new FileStream(newPath, FileMode.Create))
        {
            int data;
            byte encryptedData;

            while((data = fs1.ReadByte()) != -1)
            {
                encryptedData = (byte)(data ^ key);
                fs2.WriteByte(encryptedData);
            }

            Debug.Log($"암호화 완료: (파일 크기: {fs2.Length} bytes)");
        }
    }

    private bool DecryptFile(string path, string fileName, string newFileName)
    {
        if (!Directory.Exists(path))
        {
            Debug.Log($"Failed to decrypt: {path}가 존재하지 않습니다.");
            return false;
        }
        string filePath = Path.Combine(path, fileName);

        if (!File.Exists(filePath))
        {
            Debug.Log($"Failed to decrypt: {fileName} 파일이 존재하지 않습니다.");
            return false;
        }
        string newPath = Path.Combine(path, newFileName);
        using (FileStream fs1 = new FileStream(Path.Combine(path, fileName), FileMode.Open))
        using (FileStream fs2 = new FileStream(newPath, FileMode.Create))
        {
            int data;
            byte decryptedData;

            while((data = fs1.ReadByte()) != -1)
            {
                decryptedData = (byte)(data ^ key);
                fs2.WriteByte(decryptedData);
            }
        }
        Debug.Log("복호화 완료");
        Debug.Log($"복호화 결과: {File.ReadAllText(Path.Combine(newPath))}");
        return true;

    }

}
