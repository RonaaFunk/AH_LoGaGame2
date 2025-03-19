using UnityEngine;
using UnityEngine.UI;

namespace LoGaCulture.LUTE
{
    public class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Vector2 speed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //_image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            _image.material.mainTextureOffset += speed * Time.deltaTime;

        }
    }
}
