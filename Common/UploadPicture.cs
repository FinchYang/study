using System.Collections.Generic;

namespace Common
{

    public class UploadPicture
    {
        public string CountyCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Kind { get; set; }//ҵ������//input,out
        public int Id { get; set; }//ÿ��ҵ���ţ�Ψһ//out
        public string FileName { get; set; }//�ļ���
        public byte[] FileContent { get; set; }//ͼƬ�ļ�������������
     //   public string FileContent { get; set; }//ͼƬ�ļ�������������
    }
}