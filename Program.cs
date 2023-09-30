
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
public class Question
{
    public int Id { get; set; }
    public string SubjectCode { get; set; }
    public string Content { get; set; }
    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string AnswerC { get; set; }
    public string AnswerD { get; set; }
    public string CorrectAnswer { get; set; }
}

public class QuestionNode
{
    public Question Question { get; set; }
    public QuestionNode LeftChild { get; set; }
    public QuestionNode RightChild { get; set; }
    public int Height { get; set; }
}

public class QuestionTree
{
    private QuestionNode _root;
    public QuestionNode Root
    {
        get { return _root; }
    }
    public QuestionNode FindQuestionNodeById(QuestionNode node, int id)
    {
        if (node == null)
        {
            return null;
        }

        if (node.Question.Id == id)
        {
            return node;
        }
        else if (node.Question.Id > id)
        {
            return FindQuestionNodeById(node.LeftChild, id);
        }
        else
        {
            return FindQuestionNodeById(node.RightChild, id);
        }
    }

    public void AddQuestion(Question question)
    {
        var newNode = new QuestionNode { Question = question, Height = 1 };

        if (_root == null)
        {
            _root = newNode;
        }
        else
        {
            _root = InsertNode(_root, newNode);
        }
    }

    private QuestionNode InsertNode(QuestionNode node, QuestionNode newNode)
    {
        if (node == null)
        {
            return newNode;
        }

        if (newNode.Question.Id < node.Question.Id)
        {
            node.LeftChild = InsertNode(node.LeftChild, newNode);
        }
        else
        {
            node.RightChild = InsertNode(node.RightChild, newNode);
        }

        node.Height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;

        return BalanceNode(node);
    }

    private QuestionNode BalanceNode(QuestionNode node)
    {
        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1)
        {
            if (GetBalanceFactor(node.LeftChild) < 0)
            {
                node.LeftChild = RotateLeft(node.LeftChild);
            }
            return RotateRight(node);
        }
        else if (balanceFactor < -1)
        {
            if (GetBalanceFactor(node.RightChild) > 0)
            {
                node.RightChild = RotateRight(node.RightChild);
            }
            return RotateLeft(node);
        }

        return node;
    }

    private int GetBalanceFactor(QuestionNode node)
    {
        int leftHeight = (node.LeftChild != null) ? node.LeftChild.Height : 0;
        int rightHeight = (node.RightChild != null) ? node.RightChild.Height : 0;

        return leftHeight - rightHeight;
    }

    private QuestionNode RotateRight(QuestionNode node)
    {
        var newRoot = node.LeftChild;
        node.LeftChild = newRoot.RightChild;
        newRoot.RightChild = node;

        node.Height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;
        newRoot.Height = Math.Max(Height(newRoot.LeftChild), Height(newRoot.RightChild)) + 1;

        return newRoot;
    }

    private QuestionNode RotateLeft(QuestionNode node)
    {
        var newRoot = node.RightChild;
        node.RightChild = newRoot.LeftChild;
        newRoot.LeftChild = node;
        node.Height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;
        newRoot.Height = Math.Max(Height(newRoot.LeftChild), Height(newRoot.RightChild)) + 1;

        return newRoot;
    }


    private int Height(QuestionNode node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }
    public List<Question> GetQuestionsBySubjectCode(string subjectCode)
    {
        List<Question> questions = new List<Question>();
        TraverseInOrder(_root, subjectCode, questions);
        return questions;
    }

    private void TraverseInOrder(QuestionNode node, string subjectCode, List<Question> questions)
    {
        if (node == null)
        {
            return;
        }

        TraverseInOrder(node.LeftChild, subjectCode, questions);

        if (node.Question.SubjectCode == subjectCode)
        {
            questions.Add(node.Question);
        }

        TraverseInOrder(node.RightChild, subjectCode, questions);
    }

    public void PrintQuestions(List<Question> questions)
    {
        foreach (var question in questions)
        {
            Console.WriteLine("Id: {0}\nSubject Code: {1}\nContent: {2}\nAnswer A: {3}\nAnswer B: {4}\nAnswer C: {5}\nAnswer D: {6}\nCorrect Answer: {7}\n",
                question.Id, question.SubjectCode, question.Content, question.AnswerA, question.AnswerB, question.AnswerC, question.AnswerD, question.CorrectAnswer);
        }
    }
}

//diemthiiiiiii
class diemthi
{
    public float Diem;
    public string Mamh;
    public diemthi next;
    public SinhVien controdiemsv;
    public diemthi(string mamh,float diem)
    {
        Diem = diem;
        Mamh = mamh;
        next = null;
    }
}
class DSdiemthi
{
    diemthi head = null;
    public DSdiemthi()
    {
        head = null;
    }
    public void addhead_diemthi(string mamh,float diem)
    {
        if(head == null)
        {
            head =new diemthi(mamh,diem);
        }
        else
        {
            diemthi newdiemthi= new diemthi(mamh, diem);
            newdiemthi.next= head;
            head = newdiemthi;

        }
    }
    public void In()
    {
        diemthi current = head;
        while (current != null)
        {
            Console.Write("Mamh: {0}, Diem: {1} <---", current.Mamh, current.Diem);

            current = current.next;
        }
    }
}

class MonHoc
{
    public string MaMH;
    public string TenMH;
    public MonHoc next;

    public MonHoc(string maMH, string tenMH)
    {
        MaMH = maMH;
        TenMH = tenMH;
        next = null;
    }
}

class DanhSachMonHoc
{
    MonHoc head;

    public DanhSachMonHoc()
    {
        head = null;
    }

    public void ThemDau(string maMH, string tenMH)
    {
        if (head == null)
            head = new MonHoc(maMH, tenMH);
        else
        {
            MonHoc newMonHoc = new MonHoc(maMH, tenMH);
            newMonHoc.next = head;
            head = newMonHoc;
        }
    }
    //thêm một phương thức public trong lớp DanhSachMonHoc để truy cập vào thuộc tính head, ví dụ:
    public MonHoc GetHead()
    {
        return head;
    }


    public void InDanhSach()
    {
        MonHoc current = head;
        while (current != null)
        {
            Console.WriteLine("Ma MH: {0}, Ten MH: {1}", current.MaMH, current.TenMH);
            current = current.next;
        }
    }
    public MonHoc TimMonHoc(string maMH)
    {
        MonHoc current = head;
        while (current != null)
        {
            if (current.MaMH == maMH)
                return current;
            current = current.next;
        }
        return null;
    }

}

class Lop
{
    public string MALOP;
    public string TENLOP;
    public SinhVien con_trong_sv;
    public Lop next;
    public Lop(string malop, string tenlop)
    {
        MALOP = malop;
        TENLOP = tenlop;
        con_trong_sv = null;
        next = null;
    }
}

class SinhVien
{
    public string MASV;
    public string HO;
    public string TEN;
    public bool PHAI;
    public string PASSWORD;
    public diemthi controdiem;
    public SinhVien next;
    public SinhVien(string masv, string ho, string ten, bool phai, string password)
    {
        MASV = masv;
        HO = ho;
        TEN = ten;
        PHAI = phai;
        PASSWORD= password;
        controdiem = null;
        next = null;
    }
    public void addhead_Sinhvien(string masv, string ho, string ten, bool phai, string password)
    {
        SinhVien head = null;
        if (head == null)
        {
            head = new SinhVien(masv, ho, ten, phai, password);
        }
        else
        {
            SinhVien newsinhvien = new SinhVien(masv, ho, ten, phai, password);
            newsinhvien.next = head;
            head = newsinhvien;

        }

    }
}




class Program
    {
        static void Main()
        {
        //ds diem thi
        /*DSdiemthi dSdiemthi = new DSdiemthi();
        dSdiemthi.addhead_diemthi("MH001", 10);
        dSdiemthi.addhead_diemthi("MH001", 9);
        dSdiemthi.addhead_diemthi("MH002", 8);
        dSdiemthi.addhead_diemthi("MH002", 7);
    diemthi A = new diemthi("MH001",10);*/


        //ds mon hoc
        DanhSachMonHoc dsMonHoc = new DanhSachMonHoc();
        dsMonHoc.ThemDau("MH001", "Toan ");
        dsMonHoc.ThemDau("MH002", "Ly ");
        dsMonHoc.ThemDau("MH003", "Hoa");


 


        // tạo ra bà cô


        SinhVien gv = new SinhVien("GV000", "Le", "C", true, "GV");

        Lop lop12A = new Lop("12A", "Lop 12A");
        //"controdiem" trở thành một danh sách liên kết đơn các đối tượng "diemthi",
        //lưu trữ thông tin về điểm thi của sinh viên cho nhiều môn học khác nhau.   
        SinhVien svA = new SinhVien("SV001", "Nguyen", "A", true, "password1");//
        svA.controdiem = new diemthi("MH001", 4.0f);
        svA.controdiem.next = new diemthi("MH002", 5.5f);
        svA.controdiem.next.next = new diemthi("MH003", 7.0f);

        SinhVien svB = new SinhVien("SV002", "Tran", "B", true, "password2");
        svB.controdiem = new diemthi("MH001", 10.0f);
        svB.controdiem.next = new diemthi("MH002", 5.5f);
        svB.controdiem.next.next = new diemthi("MH003", 7.0f);

        SinhVien svC = new SinhVien("SV003", "Le", "C", true, "password3");
        svC.controdiem = new diemthi("MH001", 4.0f);
        svC.controdiem.next = new diemthi("MH002", 5.5f);
        svC.controdiem.next.next = new diemthi("MH003", 7.0f);
      
        lop12A.con_trong_sv = svA;
        svA.next = svB;
        svB.next = svC;
        
        

        Lop lop12B = new Lop("12B", "Lop 12B");
        SinhVien svE = new SinhVien("SV004", "Pham", "E", true, "password4");
        SinhVien svF = new SinhVien("SV005", "Đang", "F", true, "password5");
        SinhVien svH = new SinhVien("SV006", "Vo", "H", true, "password6");

        svE.controdiem = new diemthi("MH001", 4.0f);
        svE.controdiem.next = new diemthi("MH002", 5.0f);
        svE.controdiem.next.next = new diemthi("MH003", 8.0f);
    
        svF.controdiem = new diemthi("MH001", 4.0f);
        svF.controdiem.next = new diemthi("MH002", 5.5f);
        svF.controdiem.next.next = new diemthi("MH003", 7.7f);

       
        svH.controdiem = new diemthi("MH001", 9.0f);
        svH.controdiem.next = new diemthi("MH002", 5.5f);
        svH.controdiem.next.next = new diemthi("MH003", 7.9f);

        lop12B.con_trong_sv = svE;
        svE.next = svF;
        svF.next = svH;

        int count1 = 0;
        int count2 = 0;




        // Yêu cầu người dùng nhập mã sinh viên và password
        SinhVien A = svA;
        SinhVien B = svE;
        Console.Write("Ma sinh viên: ");
        string masv = Console.ReadLine();
       
    
        SinhVien A1 = svA;
        SinhVien B1 = svE;
        Console.Write("Password: ");
        string password = Console.ReadLine();
    




        if (masv == "GV000" && password == "GV")
        {
            Console.WriteLine("chao mung thay co den voi he thong");
                    Console.Write("nhap ma lop muon them : ");
                    string malop = Console.ReadLine();
                    Console.WriteLine("\n");
                    Console.Write("nhap ten lop muon them : ");
                    string tenlop = Console.ReadLine();
                    Lop lopmoi= new Lop(malop, tenlop);

            Console.Write("nhap so luong sinh vien muon them zo:");   
            int n=int.Parse(Console.ReadLine());
            while (n > 0)
            {
                Console.Write("nhap ma sinh vien muon them : ");
                string Masv = Console.ReadLine();
                Console.WriteLine("\n");
                Console.Write("nhap Ho muon them  : ");
                string Ho = Console.ReadLine();
                Console.WriteLine("\n");
                Console.Write("nhap Ten muon them  : ");
                string Ten = Console.ReadLine();
                Console.WriteLine("\n");
                Console.Write("nhap Password muon them  : ");
                string Password = Console.ReadLine();
                Console.WriteLine("\n");
                Console.Write("Giới tính (Nam - true, Nữ - false): ");
                bool phai = bool.Parse(Console.ReadLine());
                SinhVien svMoi = new SinhVien(Masv, Ho, Ten, phai, password);
                n--;

                Console.WriteLine("\n");
                Console.WriteLine(" Ban muon them hoc sinh moi vao lop nao ? ");
                Console.Write("nhap ma lop : ");
                string ma = Console.ReadLine();
                Console.WriteLine("\n");
                Console.Write("nhap ten lop : ");
                string ten = Console.ReadLine();
                Console.WriteLine("\n");


                if (ma == "12A" && ten == "lop12A")// tính tuyến
                {
                    svC.next = svMoi;
                    SinhVien current = svA;
                    Console.WriteLine("sau day la danh sach lop12A khi them sinh vien vao");
                    while (current != null)
                    {

                        Console.WriteLine("{0} {1} {2} ", current.MASV, current.HO, current.TEN);
                        current = current.next;
                    }
                }


                else if (ma == "12B" && ten == "lop12B")
                {
                    svH.next = svMoi;
                    SinhVien current = svE;
                    Console.WriteLine("sau day la danh sach lop12B khi them sinh vien vao");
                    while (current != null)
                    {

                        Console.WriteLine("{0} {1} {2} ", current.MASV, current.HO, current.TEN);
                        current = current.next;
                    }
                }

                else if (ma == malop && ten == tenlop)
                {
                    lopmoi.con_trong_sv = svMoi;
                    SinhVien current = svMoi;
                    Console.WriteLine("sau day la danh sach lop12C khi them sinh vien vao");
                    while (current != null)
                    {

                        Console.WriteLine("{0} {1} {2} ", current.MASV, current.HO, current.TEN);
                        current = current.next;
                    }

                }
            }





        }

        // bắt đầu từ đâydành cho mấy thg học sinh
        else
        {
            // Duyệt danh sách sinh viên
            SinhVien currentA = svA;
            // diemthi current1 =  svA.controdiem; dùng vậy chỉ trỏ dc thg A ví dụ nhập 003 thì nó vẫn ra thg A
            MonHoc currentmonHoc = dsMonHoc.GetHead();
            while (currentA != null)
            {
                if (currentA.MASV == masv && currentA.PASSWORD == password)
                {
                    Console.WriteLine("{0} {1} ", currentA.HO, currentA.TEN);
                    /*diemthi current1 = currentA.controdiem;  //in ra thg sinh vien bat ki khi minh nhap zo
                    while(current1 != null) {   
                        Console.WriteLine("Ma môn học: {0}, Điem: {1}", current1.Mamh, current1.Diem);
                        current1 = current1.next;
                    }*/


                    /* while (current1 != null)  // in diem cho  sinh viên mongmuốn
                     {
                         Console.WriteLine("Ma môn học: {0}, Điem: {1}", current1.Mamh, current1.Diem);
                         current1 = svA.controdiem;

                     }*/
                    // Nhap ma mon hoc
                    Console.WriteLine("Ma mon hoc : ");
                    string MaMH = Console.ReadLine();
                    MonHoc current = dsMonHoc.GetHead();
                    while (current != null)
                    {
                        if (current.MaMH == MaMH)
                        {
                            Console.WriteLine(current.TenMH);
                            break;
                        }
                        current = current.next;
                        count1++;

                    }


                }
                currentA = currentA.next;
            }


            // cái này thì cho lớp 12B
            SinhVien currentB = svE;
            // diemthi current2 = svB.controdiem;
            while (currentB != null)
            {
                if (currentB.MASV == masv && currentB.PASSWORD == password)
                {
                    Console.WriteLine("{0} {1} ", currentB.HO, currentB.TEN);
                    /* diemthi current2 = currentB.controdiem;

                     while (current2 != null)
                     {
                         Console.WriteLine("Ma môn học: {0}, Điem: {1}", current2.Mamh, current2.Diem);
                         current2 = current2.next;
                     }*/
                    Console.WriteLine("Ma mon hoc : ");
                    string MaMH = Console.ReadLine();
                    MonHoc current3 = dsMonHoc.GetHead();
                    while (current3 != null)
                    {
                        if (current3.MaMH == MaMH)
                        {
                            Console.WriteLine("sau day la cau hoi mon:");
                            Console.WriteLine(current3.TenMH);

                            break;
                        }
                        current3 = current3.next;
                        count2++;

                    }


                }
                currentB = currentB.next;

            }

            //TEST CAI COUNT  Console.WriteLine("{0}", count1);

            if (count1 == 2 || count2 == 2)
            {
                var questionTreeToan = new QuestionTree();

                int numCorrectAnswers = 0;
                // Thêm các câu hỏi vào cây
                questionTreeToan.AddQuestion(new Question
                {
                    Id = 1,
                    SubjectCode = "Math",
                    Content = "What is the result of 2+2?",
                    AnswerA = "1",
                    AnswerB = "2",
                    AnswerC = "3",
                    AnswerD = "4",
                    CorrectAnswer = "D"
                });
                questionTreeToan.AddQuestion(new Question
                {
                    Id = 2,
                    SubjectCode = "Math",
                    Content = "What is the result of 5*5?",
                    AnswerA = "20",
                    AnswerB = "25",
                    AnswerC = "30",
                    AnswerD = "35",
                    CorrectAnswer = "B"
                });
                questionTreeToan.AddQuestion(new Question
                {
                    Id = 3,
                    SubjectCode = "Science",
                    Content = "What is the boiling point of water?",
                    AnswerA = "100 degrees Fahrenheit",
                    AnswerB = "100 degrees Celsius",
                    AnswerC = "212 degrees Fahrenheit",
                    AnswerD = "212 degrees Celsius",
                    CorrectAnswer = "B"
                });
                questionTreeToan.AddQuestion(new Question
                {
                    Id = 4,
                    SubjectCode = "Science",
                    Content = "What is the symbol for iron?",
                    AnswerA = "Ir",
                    AnswerB = "Fe",
                    AnswerC = "In",
                    AnswerD = "I",
                    CorrectAnswer = "B"
                });
                questionTreeToan.AddQuestion(new Question
                {
                    Id = 5,
                    SubjectCode = "History",
                    Content = "Who was the first president of the United States?",
                    AnswerA = "George Washington",
                    AnswerB = "Thomas Jefferson",
                    AnswerC = "John Adams",
                    AnswerD = "Abraham Lincoln",
                    CorrectAnswer = "A"
                });




                // Chọn đáp án trắc nghiệm cho các câu hỏi
                Console.WriteLine("moi ban nhap so cau hoi muon thi : ");
                int n = int.Parse(Console.ReadLine());

                Console.WriteLine("nhap so phut ban muon thi biet rang 1 cau thi toi da 5p ");
                float m = float.Parse(Console.ReadLine());

                Console.WriteLine("neu ban dang o cau hoi ma het gio ban co the hoan thanh no va duoc diem cau do ");


                while (n <= 0 || n > 5)
                {
                    Console.WriteLine("moi ban nhap so cau hoi muon thi biet rang chi co 5 cau hoi: ");
                    n = int.Parse(Console.ReadLine());
                }
                while (m <= 0 || m > 60 || m > n * 5)
                {
                    Console.WriteLine(" nhin cho ki noi quy thi 1 cau trac nghiem");
                    Console.WriteLine(" hoac ban qua ngu ngoc nhap qua 60p hoac 0phut :)))");
                    m = float.Parse(Console.ReadLine());
                }
                // Lấy thời gian bắt đầu bài tập
                DateTime startTime = DateTime.Now;

                // Tính thời gian kết thúc bài tập bằng cách cộng thời gian bắt đầu với số phút cho trước
                DateTime endTime = startTime.AddMinutes(m);
                int count = 0;

                // Kiểm tra thời gian hiện tại có lớn hơn hoặc bằng thời gian kết thúc hay không
                while (DateTime.Now < endTime)
                {

                    // Bài tập đang được làm, chờ đến khi hết giờ
                    for (int i = 1; i <= n; i++)
                    {
                        Console.WriteLine("Question {0}:", i);
                        var question = questionTreeToan.FindQuestionNodeById(questionTreeToan.Root, i).Question;
                        Console.WriteLine(question.Content);
                        Console.WriteLine("A. {0}", question.AnswerA);
                        Console.WriteLine("B. {0}", question.AnswerB);
                        Console.WriteLine("C. {0}", question.AnswerC);
                        Console.WriteLine("D. {0}", question.AnswerD);
                        Console.Write("Your answer: ");
                        var userAnswer = Console.ReadLine().ToUpper();
                        if (userAnswer == question.CorrectAnswer)
                        {

                            Console.WriteLine("Correct!");
                            numCorrectAnswers++;
                            count++;
                        }
                        else
                        {
                            count++;
                            Console.WriteLine("Incorrect! The correct answer is {0}.", question.CorrectAnswer);
                        }
                        if (DateTime.Now >= endTime || count == n)
                        {
                            break;
                        }
                    }
                    if (DateTime.Now >= endTime || count == n)
                    {

                        break;
                    }
                }
                if (count == n)
                {
                    Console.WriteLine("ban da hoan thanh truoc {0} phut that ge gom", DateTime.Now - endTime);
                }
                else if (count != n)
                {
                    Console.WriteLine("ban khong the hoan thanh phan thi trong {0} phut", m);
                }

                // Thời gian đã hết, dừng bài tập



                // Tính điểm dựa trên số câu trả lời đúng và tổng số câu hỏi
                int numQuestionsToan = 5; // Tổng số câu hỏi
                int scoreToan = numCorrectAnswers * 10 / numQuestionsToan;


                SinhVien currentAA = svA;
                while (currentAA != null)
                {
                    if (currentAA.MASV == masv && currentAA.PASSWORD == password)
                    {
                        diemthi current1 = currentAA.controdiem;
                        current1.Diem = scoreToan;
                        Console.WriteLine("Your score: {0}", current1.Diem);
                        break;
                    }
                    currentAA = currentAA.next;

                }

                SinhVien currentBB = svE;
                while (currentBB != null)
                {
                    if (currentBB.MASV == masv && currentBB.PASSWORD == password)
                    {
                        diemthi current2 = currentBB.controdiem;
                        current2.Diem = scoreToan;
                        Console.WriteLine("Your score: {0}", current2.Diem);
                        break;
                    }
                    currentBB = currentBB.next;

                }

            }
            /////////////////////////////////////////
            //cau hoi Li
            else if (count1 == 1 || count2 == 1)
            {
                var questionTreeLi = new QuestionTree();

                int numCorrectAnswers2 = 0;
                // Thêm các câu hỏi vào cây
                questionTreeLi.AddQuestion(new Question
                {
                    Id = 1,
                    SubjectCode = "Li",
                    Content = "Pha ban đầu cho phép xác định?",
                    AnswerA = "trạng thái của dao động ở thời điểm ban đầu",
                    AnswerB = " vận tốc của dao động ở thời điểm t bất kỳ",
                    AnswerC = " ly độ của dao động ở thời điểm t bất kỳ",
                    AnswerD = " gia tốc của dao động ở thời điểm t bất kỳ.",
                    CorrectAnswer = "B"
                });
                questionTreeLi.AddQuestion(new Question
                {
                    Id = 2,
                    SubjectCode = "Li",
                    Content = " Con lắc lò xo dao động diều hòa có tốc độ bằng 0 khi vật ở vị trí:",
                    AnswerA = ". mà hợp lực tác dụng vào vật bằng 0.",
                    AnswerB = "mà lò xo không biến dạng",
                    AnswerC = "có li độ bằng 0",
                    AnswerD = "gia tốc có độ lớn cực đại",
                    CorrectAnswer = "D"
                });
                questionTreeLi.AddQuestion(new Question
                {
                    Id = 3,
                    SubjectCode = "Li",
                    Content = "What is the boiling point of water?",
                    AnswerA = "100 degrees Fahrenheit",
                    AnswerB = "100 degrees Celsius",
                    AnswerC = "212 degrees Fahrenheit",
                    AnswerD = "212 degrees Celsius",
                    CorrectAnswer = "B"
                });
                questionTreeLi.AddQuestion(new Question
                {
                    Id = 4,
                    SubjectCode = "Li",
                    Content = "Hiện tượng cộng hưởng thể hiện rõ rết nhất khi:?",
                    AnswerA = "biên độ của lực cưỡng bức nhỏ.",
                    AnswerB = "tần số của lực cưỡng bức lớn",
                    AnswerC = "lực ma sát của môi trường lớn",
                    AnswerD = "lực ma sát của môi trường nhỏ.",
                    CorrectAnswer = "D"
                });
                questionTreeLi.AddQuestion(new Question
                {
                    Id = 5,
                    SubjectCode = "Li",
                    Content = "Trong hiện tượng sóng dừng, khoảng cách giữa hai nút sóng cạnh nhau bằng:?",
                    AnswerA = "một phần tư bước sóng.",
                    AnswerB = "hai lần bước sóng",
                    AnswerC = " nửa bước sóng.",
                    AnswerD = "4 lần bước sóng",
                    CorrectAnswer = "C"
                });
                Console.WriteLine("moi ban nhap so cau hoi muon thi : ");
                int n = int.Parse(Console.ReadLine());


                Console.WriteLine("nhap so phut ban muon thi biet rang 1 cau thi toi da 5p ");
                float m = float.Parse(Console.ReadLine());

                Console.WriteLine("neu ban dang o cau hoi ma het gio ban co the hoan thanh no va duoc diem cau do ");


                while (n <= 0 || n > 5)
                {
                    Console.WriteLine("moi ban nhap so cau hoi muon thi biet rang chi co 5 cau hoi: ");
                    n = int.Parse(Console.ReadLine());
                }
                while (m <= 0 || m > 60 || m > n * 5)
                {
                    Console.WriteLine(" nhin cho ki noi quy thi 1 cau trac nghiem");
                    Console.WriteLine(" hoac ban qua ngu ngoc nhap qua 60p hoac 0phut :)))");
                    m = float.Parse(Console.ReadLine());
                }
                // Lấy thời gian bắt đầu bài tập
                DateTime startTime = DateTime.Now;

                // Tính thời gian kết thúc bài tập bằng cách cộng thời gian bắt đầu với số phút cho trước
                DateTime endTime = startTime.AddMinutes(m);
                int count = 0;

                // Kiểm tra thời gian hiện tại có lớn hơn hoặc bằng thời gian kết thúc hay không
                while (DateTime.Now < endTime)
                {

                    // Bài tập đang được làm, chờ đến khi hết giờ
                    for (int i = 1; i <= n; i++)
                    {
                        Console.WriteLine("Question {0}:", i);
                        var question = questionTreeLi.FindQuestionNodeById(questionTreeLi.Root, i).Question;
                        Console.WriteLine(question.Content);
                        Console.WriteLine("A. {0}", question.AnswerA);
                        Console.WriteLine("B. {0}", question.AnswerB);
                        Console.WriteLine("C. {0}", question.AnswerC);
                        Console.WriteLine("D. {0}", question.AnswerD);
                        Console.Write("Your answer: ");
                        var userAnswer = Console.ReadLine().ToUpper();
                        if (userAnswer == question.CorrectAnswer)
                        {

                            Console.WriteLine("Correct!");
                            numCorrectAnswers2++;
                            count++;
                        }
                        else
                        {
                            count++;
                            Console.WriteLine("Incorrect! The correct answer is {0}.", question.CorrectAnswer);
                        }
                        if (DateTime.Now >= endTime || count == n)
                        {
                            break;
                        }
                    }
                    if (DateTime.Now >= endTime || count == n)
                    {

                        break;
                    }
                }
                if (count == n)
                {
                    Console.WriteLine("ban da hoan thanh truoc {0} phut that ge gom", DateTime.Now - endTime);
                }
                else if (count != n)
                {
                    Console.WriteLine("ban khong the hoan thanh phan thi trong {0} phut", m);
                }

                // Thời gian đã hết, dừng bài tập

                // Tính điểm dựa trên số câu trả lời đúng và tổng số câu hỏi
                int numQuestionsLi = 5; // Tổng số câu hỏi
                int scoreLi = numCorrectAnswers2 * 10 / numQuestionsLi;

                SinhVien currentAA = svA;
                while (currentAA != null)
                {
                    if (currentAA.MASV == masv && currentAA.PASSWORD == password)
                    {
                        diemthi current1 = currentAA.controdiem.next;
                        current1.Diem = scoreLi;
                        Console.WriteLine("Your score: {0}", current1.Diem);
                        break;
                    }
                    currentAA = currentAA.next;

                }

                SinhVien currentBB = svE;
                while (currentBB != null)
                {
                    if (currentBB.MASV == masv && currentBB.PASSWORD == password)
                    {
                        diemthi current2 = currentBB.controdiem.next;
                        current2.Diem = scoreLi;
                        Console.WriteLine("Your score: {0}", current2.Diem);
                        break;
                    }
                    currentBB = currentBB.next;

                }

            }
            //cau hoi Hoa

            else if (count1 == 0 || count2 == 0)
            {



                var questionTreeHoa = new QuestionTree();

                int numCorrectAnswers3 = 0;
                // Thêm các câu hỏi vào cây
                questionTreeHoa.AddQuestion(new Question
                {
                    Id = 1,
                    SubjectCode = "Hoa",
                    Content = "glixetol cho zo Cu(OH)2 co mau gi",
                    AnswerA = "xanh nhat",
                    AnswerB = " vang",
                    AnswerC = " do",
                    AnswerD = " nau",
                    CorrectAnswer = "A"
                });
                questionTreeHoa.AddQuestion(new Question
                {
                    Id = 2,
                    SubjectCode = "Hoa",
                    Content = " M=7 la",
                    AnswerA = "Li.",
                    AnswerB = "K",
                    AnswerC = "Na",
                    AnswerD = "Ra",
                    CorrectAnswer = "Li"
                });
                questionTreeHoa.AddQuestion(new Question
                {
                    Id = 3,
                    SubjectCode = "Hoa",
                    Content = "What is the boiling point of water?",
                    AnswerA = "100 degrees Fahrenheit",
                    AnswerB = "100 degrees Celsius",
                    AnswerC = "212 degrees Fahrenheit",
                    AnswerD = "212 degrees Celsius",
                    CorrectAnswer = "B"
                });
                questionTreeHoa.AddQuestion(new Question
                {
                    Id = 4,
                    SubjectCode = "Hoa",
                    Content = "Hiện tượng cộng hưởng thể hiện rõ rết nhất khi:?",
                    AnswerA = "biên độ của lực cưỡng bức nhỏ.",
                    AnswerB = "tần số của lực cưỡng bức lớn",
                    AnswerC = "lực ma sát của môi trường lớn",
                    AnswerD = "lực ma sát của môi trường nhỏ.",
                    CorrectAnswer = "D"
                });
                questionTreeHoa.AddQuestion(new Question
                {
                    Id = 5,
                    SubjectCode = "Hoa",
                    Content = "cong thu glucozo?",
                    AnswerA = "c6h12o6.",
                    AnswerB = "c12h22o11",
                    AnswerC = " c6h10o5",
                    AnswerD = "c6h12o4",
                    CorrectAnswer = "A"
                });

                // Chọn đáp án trắc nghiệm cho các câu hỏi

                Console.WriteLine("moi ban nhap so cau hoi muon thi : ");
                int n = int.Parse(Console.ReadLine());

                Console.WriteLine("nhap so phut ban muon thi biet rang 1 cau thi toi da 5p ");
                float m = float.Parse(Console.ReadLine());

                Console.WriteLine("neu ban dang o cau hoi ma het gio ban co the hoan thanh no va duoc diem cau do ");


                while (n <= 0 || n > 5)
                {
                    Console.WriteLine("moi ban nhap so cau hoi muon thi biet rang chi co 5 cau hoi: ");
                    n = int.Parse(Console.ReadLine());
                }
                while (m <= 0 || m > 60 || m > n * 5)
                {
                    Console.WriteLine(" nhin cho ki noi quy thi 1 cau trac nghiem");
                    Console.WriteLine(" hoac ban qua ngu ngoc nhap qua 60p hoac 0phut :)))");
                    m = float.Parse(Console.ReadLine());
                }
                // Lấy thời gian bắt đầu bài tập
                DateTime startTime = DateTime.Now;

                // Tính thời gian kết thúc bài tập bằng cách cộng thời gian bắt đầu với số phút cho trước
                DateTime endTime = startTime.AddMinutes(m);
                int count = 0;

                // Kiểm tra thời gian hiện tại có lớn hơn hoặc bằng thời gian kết thúc hay không
                while (DateTime.Now < endTime)
                {

                    // Bài tập đang được làm, chờ đến khi hết giờ
                    for (int i = 1; i <= n; i++)
                    {
                        Console.WriteLine("Question {0}:", i);
                        var question = questionTreeHoa.FindQuestionNodeById(questionTreeHoa.Root, i).Question;
                        Console.WriteLine(question.Content);
                        Console.WriteLine("A. {0}", question.AnswerA);
                        Console.WriteLine("B. {0}", question.AnswerB);
                        Console.WriteLine("C. {0}", question.AnswerC);
                        Console.WriteLine("D. {0}", question.AnswerD);
                        Console.Write("Your answer: ");
                        var userAnswer = Console.ReadLine().ToUpper();
                        if (userAnswer == question.CorrectAnswer)
                        {

                            Console.WriteLine("Correct!");
                            numCorrectAnswers3++;
                            count++;
                        }
                        else
                        {
                            count++;
                            Console.WriteLine("Incorrect! The correct answer is {0}.", question.CorrectAnswer);
                        }
                        if (DateTime.Now >= endTime || count == n)
                        {
                            break;
                        }
                    }
                    if (DateTime.Now >= endTime || count == n)
                    {

                        break;
                    }
                }

                Console.WriteLine("\n");
                if (count == n)
                {
                    Console.WriteLine("ban da hoan thanh truoc {0} phut that ge gom", DateTime.Now - endTime);

                }
                else if (count != n)
                {
                    Console.WriteLine("ban khong the hoan thanh phan thi trong {0} phut", m);
                }


                // Thời gian đã hết, dừng bài tập

                // Tính điểm dựa trên số câu trả lời đúng và tổng số câu hỏi
                int numQuestionsHoa = 5; // Tổng số câu hỏi
                int scoreHoa = numCorrectAnswers3 * 10 / numQuestionsHoa;
                Console.WriteLine("Your score: {0}", scoreHoa);


                //lưu điểm cho nó
                SinhVien currentAA = svA;
                while (currentAA!= null)
                {
                    if (currentAA.MASV == masv && currentAA.PASSWORD == password)
                    {
                        diemthi current1 = currentAA.controdiem.next.next;
                        current1.Diem = scoreHoa;
                        Console.WriteLine("Your score: {0}", current1.Diem);
                        break;
                    }
                    currentAA = currentAA.next;

                }

                SinhVien currentBB= svE;
                while (currentBB!= null)
                {
                    if (currentBB.MASV == masv && currentBB.PASSWORD == password)
                    {
                        diemthi current2 = currentB.controdiem.next.next;
                        current2.Diem = scoreHoa;
                        Console.WriteLine("Your score: {0}", current2.Diem);
                        break;
                    }
                    currentBB= currentBB.next;

                }
            }

            Console.WriteLine("\n");
            //////////////////////////////////////
            /////chúc mừng r bắt nó dò điểm
            ///
            SinhVien currentAa = svA;
            while (currentAa != null)
            {
                if (currentAa.MASV == masv && currentAa.PASSWORD == password)
                {
                    Console.WriteLine(" chuc mung em {0} {1} da hoan thanh tot bai thi", currentAa.HO, currentAa.TEN);
                    Console.WriteLine("diem cua em da duoc cap nhat vui long xem diem tai danh sach lop sau va kiem tra");
                }
                currentAa = currentAa.next;

            }

            SinhVien currentBb = svE;
            while (currentBb != null)
            {
                if (currentBb.MASV == masv && currentBb.PASSWORD == password)
                {
                    Console.WriteLine(" chuc mung em {0} {1} da hoan thanh tot bai thi", currentBb.HO, currentBb.TEN);
                    Console.WriteLine("diem cua em da duoc cap nhat vui long xem diem tai danh sach lop sau va kiem tra");
                }
                currentBb = currentBb.next;

            }


            Console.WriteLine("\n");
            //////////////////////////////////////
            ///// xuất cái bảng diem863 từng lớp
            Console.WriteLine("danh sach {0}", lop12A.TENLOP);
            SinhVien currentA1 = svA;
            while (currentA1 != null)
            {
                Console.WriteLine("{0} {1} ", currentA1.HO, currentA1.TEN);
                diemthi current1 = currentA1.controdiem;  //in ra thg sinh vien bat ki khi minh nhap zo
                while (current1 != null)
                {
                    Console.WriteLine("Ma mon hoc: {0}, Điem: {1}", current1.Mamh, current1.Diem);
                    current1 = current1.next;
                }
                currentA1 = currentA1.next;
            }





            Console.WriteLine("\n");



            ////////////
            //////////////
            ///lưu zo file
          
                Console.WriteLine("Danh sách {0}", lop12B.TENLOP);
                SinhVien currentB1 = svE;
                while (currentB1 != null)
                {
                    Console.WriteLine("{0} {1}", currentB1.HO, currentB1.TEN);
                    diemthi current1 = currentB1.controdiem;
                    while (current1 != null)
                    {
                        Console.WriteLine("Ma mon hoc: {0}, Điem: {1}", current1.Mamh, current1.Diem);
                        current1 = current1.next;
                    }
                    currentB1 = currentB1.next;
                }
            
        }

        Console.ReadLine();

        }






    }

