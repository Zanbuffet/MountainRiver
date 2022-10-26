using UnityEngine;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class PlayVideo : MonoBehaviour
{
 
    public GameObject StartMove;//启动动画
        private void Awake()
        {
            if (StartMove != null)//播放启动动画
            {
                VideoPlayer videoPlayer = StartMove.GetComponent<VideoPlayer>();
                StartMove.SetActive(true);//默认对象关闭
                //videoPlayer.frame = 100;//跳过前100帧
                videoPlayer.loopPointReached += VideoPlayer_loopPointReached;//添加播放结束事件
                StartMove.transform.SetAsLastSibling();
                videoPlayer.Play();//播放视频
            }
            else
                Debug.Log("启动动画 StartMove=null");
        }

        /// <summary>
        /// 播放结束或播放到循环的点时被执行
        /// </summary>
        private void VideoPlayer_loopPointReached(VideoPlayer source)
        {
            if (StartMove != null)
            {
                StartMove.SetActive(false);
                GameObject.Destroy(StartMove);
                StartMove = null;
                Destroy(GameObject.Find("CoverCanvas"));
                int index = SceneManager.GetActiveScene().buildIndex + 1;

                if (index >= SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(0);
                else
                SceneManager.LoadScene(index);
            }
            else
                Debug.Log("启动动画 播放结束 StartMove=null");
        }

    
}
