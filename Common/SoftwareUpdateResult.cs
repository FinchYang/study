namespace Common
{
    public class SoftwareUpdateResult: CommonResult
    {
        public string NewVersionFileName { get; set; }// empty����û���°汾����������°汾�ļ���
        public byte[] FileContent { get; set; }//�ļ�������������
    }
}