using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class UdpReceiver : MonoBehaviour
{
    public List<float> keypoints = new List<float>();
    UdpClient client;
    Thread receiveThread;

    void Start()
    {
        client = new UdpClient(5055); // 포트 확인
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void ReceiveData()
    {
        while (true)
        {
            IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = client.Receive(ref anyIP);
            string text = Encoding.UTF8.GetString(data);
            text = text.Trim('[', ']');
            string[] tokens = text.Split(',');

            keypoints.Clear();
            foreach (var token in tokens)
            {
                if (float.TryParse(token, out float val))
                    keypoints.Add(val);
            }

            // ? 이거 꼭 넣어줘!
            Debug.Log("Received keypoints: " + keypoints.Count);
        }
    }
}
