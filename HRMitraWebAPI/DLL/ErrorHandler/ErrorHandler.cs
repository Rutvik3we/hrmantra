using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections;

namespace NErrorHandler
{
    public class ErrorHandler : Exception
    {
        private Stack _myStack;
        private string _procName;
        private string _fileName;
        private string _machineName;
        private string _fullFileName;
        private bool _isDisposed;
        private FileInfo objFileInfo;
        // Private objFileStream As System.IO.FileStream
        // Private objWriter As System.IO.StreamWriter
        private ReaderWriterLock _readerWriterLock;
        private const Int32 MAX_ERROR_FILE_SIZE = 1024;
        private StringBuilder _errStr;

        public ErrorHandler(string processName, string fName = "C:/ErrorLog/")
        {
            if (!fName.EndsWith("/"))
                _fileName = fName + "/" + processName;
            else
                _fileName = fName + processName;

            _procName = processName; // Process.GetCurrentProcess.ProcessName
            GetFileInfo();
            _machineName = ".";
            _readerWriterLock = new ReaderWriterLock();
            // _isDisposed = False
            _myStack = new Stack();
            _errStr = new StringBuilder();
        }

        public string ProcessName
        {
            get
            {
                return _procName;
            }
            set
            {
                _procName = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    GetFileInfo();
                    _machineName = ".";
                }
            }
        }

        public string MachineName
        {
            get
            {
                return _machineName;
            }
            set
            {
                _machineName = value;
            }
        }


        public void WritetoLogFile(Exception ex, string str)
        {
            // 10.05.06. | Mihir | Improved the code using StringBuilder
            // Dim errStr As String
            System.IO.FileStream objFileStream;
            System.IO.StreamWriter objWriter;

            try
            {
                _readerWriterLock.AcquireWriterLock(3000);

                CheckSize();

                objFileStream = new System.IO.FileStream(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                objWriter = new System.IO.StreamWriter(objFileStream);

                if (objWriter != null)
                {
                    _errStr.Remove(0, _errStr.Length);
                    // errStr = _procName & "  " & Now.ToString & Environment.NewLine & " Exception : " & ex.Message & "  " & str & Environment.NewLine
                    _errStr.Append(_procName).Append("  ").Append(DateTime.Now.ToString()).Append(Environment.NewLine).Append(" Exception : ").Append(ex.Message).Append("  ").Append(str).Append(Environment.NewLine);
                    string dummyString;
                    dummyString = _errStr.ToString();
                    objWriter.WriteLine(dummyString);
                    dummyString = null;
                }

                if (objWriter != null)
                {
                    objWriter.Flush();
                    objWriter.Close();
                    objWriter = null;
                }
                if (objFileStream != null)
                {
                    // objFileStream.Flush()
                    objFileStream.Close();
                    objFileStream = null;
                }
            }
            catch
            {
            }
            // GenerateEventLog(_procName, ".", exc.ToString, EventLogEntryType.Error, "DITAS")
            finally
            {
                if (_readerWriterLock.IsWriterLockHeld)
                    _readerWriterLock.ReleaseWriterLock();
            }
        }

        public void WritetoLogFile(Exception ex)
        {
            // 10.05.06. | Mihir | Improved the code using StringBuilder
            // Dim errStr As String
            System.IO.FileStream objFileStream;
            System.IO.StreamWriter objWriter;
            try
            {
                _readerWriterLock.AcquireWriterLock(3000);

                CheckSize();
                objFileStream = new System.IO.FileStream(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                objWriter = new System.IO.StreamWriter(objFileStream);

                if (objWriter != null)
                {
                    // errStr = _procName & "  " & Now.ToString & Environment.NewLine & ex.ToString
                    _errStr.Remove(0, _errStr.Length);
                    _errStr.Append(_procName).Append("  ").Append(DateTime.Now.ToString()).Append(Environment.NewLine).Append(ex.ToString());
                    string dummyString;
                    dummyString = _errStr.ToString();
                    objWriter.WriteLine(dummyString);
                    dummyString = null;
                }

                if (objWriter != null)
                {
                    objWriter.Flush();
                    objWriter.Close();
                    objWriter = null;
                }
                if (objFileStream != null)
                {
                    // objFileStream.Flush()
                    objFileStream.Close();
                    objFileStream = null;
                }
            }
            catch
            {
            }
            // GenerateEventLog(_procName, ".", exc.ToString, EventLogEntryType.Error, "DITAS")
            finally
            {
                if (_readerWriterLock.IsWriterLockHeld)
                    _readerWriterLock.ReleaseWriterLock();
            }
        }

        public void WritetoLogFile(string str, bool withTimeStamp = true)
        {
            System.IO.FileStream objFileStream;
            System.IO.StreamWriter objWriter;
            // Dim objWriter As FileStreamWriter

            try
            {
                _readerWriterLock.AcquireWriterLock(3000);


                CheckSize();
                objFileStream = new System.IO.FileStream(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                objWriter = new System.IO.StreamWriter(objFileStream);

                // objWriter = New FileStreamWriter(objFileStream)
                // objWriter.AutoFlush = False

                if (objWriter != null)
                {
                    _errStr.Remove(0, _errStr.Length);
                    if (withTimeStamp)
                        // str = _procName & "  " & Now.ToString & Environment.NewLine & str
                        _errStr.Append(_procName).Append("  ").Append(DateTime.Now.ToString()).Append(Environment.NewLine).Append(str);
                    else
                        _errStr.Append(str);
                    string dummyString;
                    dummyString = _errStr.ToString();
                    objWriter.WriteLine(dummyString);
                    dummyString = null;
                }

                if (objWriter != null)
                {
                    objWriter.Flush();
                    objWriter.Close();
                    // objWriter.Dispose()
                    objWriter = null;
                }
                if (objFileStream != null)
                {
                    // objFileStream.Flush()
                    objFileStream.Close();
                    objFileStream = null;
                }
            }
            catch
            {
            }
            // GenerateEventLog(_procName, ".", ex.ToString, EventLogEntryType.Error, "DITAS")
            finally
            {
                if (_readerWriterLock.IsWriterLockHeld)
                    _readerWriterLock.ReleaseWriterLock();
            }
        }

        public void WritetoLogFile(string str, byte[] myByte)
        {
            System.IO.FileStream objFileStream;
            System.IO.StreamWriter objWriter;

            try
            {
                _readerWriterLock.AcquireWriterLock(3000);

                CheckSize();
                objFileStream = new System.IO.FileStream(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                objWriter = new System.IO.StreamWriter(objFileStream);

                if (objWriter != null)
                {
                    _errStr.Remove(0, _errStr.Length);

                    _errStr.Append(str).Append(System.BitConverter.ToString(myByte));
                    string dummyString;
                    dummyString = _errStr.ToString();
                    objWriter.WriteLine(dummyString);
                    dummyString = null;
                }

                if (objWriter != null)
                {
                    objWriter.Flush();
                    objWriter.Close();
                    objWriter = null;
                }
                if (objFileStream != null)
                {
                    // objFileStream.Flush()
                    objFileStream.Close();
                    objFileStream = null;
                }
            }
            catch
            {
            }
            // GenerateEventLog(_procName, ".", ex.ToString, EventLogEntryType.Error, "DITAS")
            finally
            {
                if (_readerWriterLock.IsWriterLockHeld)
                    _readerWriterLock.ReleaseWriterLock();
            }
        }

        public void WritetoLogFile(string str, byte[] myByte, string newStr)
        {
            System.IO.FileStream objFileStream;

            FileStreamWriter objWriter;

            try
            {
                _readerWriterLock.AcquireWriterLock(3000);

                CheckSize();
                objFileStream = new System.IO.FileStream(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                objWriter = new FileStreamWriter(objFileStream);

                if (objWriter != null)
                {
                    // temp
                    // 'errStr = str & System.BitConverter.ToString(myByte) & newStr
                    _errStr.Remove(0, _errStr.Length);
                    _errStr.Append(str).Append(System.BitConverter.ToString(myByte)).Append(newStr);
                    string dummyString;
                    dummyString = _errStr.ToString();
                    objWriter.WriteLine(dummyString);
                    dummyString = null;
                }

                if (objWriter != null)
                {
                    // objWriter.Flush()
                    // objWriter.Close()
                    objWriter.Dispose();
                    objWriter = null;
                }
                if (objFileStream != null)
                {
                    // objFileStream.Flush()
                    objFileStream.Close();
                    objFileStream = null;
                }
            }
            catch
            {
            }
            // GenerateEventLog(_procName, ".", ex.ToString, EventLogEntryType.Error, "DITAS")
            finally
            {
                if (_readerWriterLock.IsWriterLockHeld)
                    _readerWriterLock.ReleaseWriterLock();
            }
        }

        public void GetFileInfo()
        {
            _fullFileName = GetFileName();
            try
            {
                DirectoryInfo objDirectoryInfo;
                objDirectoryInfo = System.IO.Directory.GetParent(_fullFileName);
            }
            catch
            {
            }

            finally
            {
            }
            if (objFileInfo != null)
                objFileInfo = null;

            objFileInfo = new FileInfo(_fullFileName);
            CheckSize();
        }

        private void CheckSize()
        {
            try
            {
                objFileInfo.Refresh();
                if (objFileInfo.Exists == true)
                {
                    if (objFileInfo.Length >= MAX_ERROR_FILE_SIZE * 1024 * 2)
                    {
                        string destFile = _fullFileName;

                        destFile = destFile.Substring(0, _fullFileName.Length - 4) + "_OLD.txt";
                        objFileInfo.CopyTo(destFile, true);
                        objFileInfo.Delete();
                        destFile = "";
                        destFile = null;
                    }
                }
            }
            catch
            {
            }
            // GenerateEventLog(_procName, ".", ex.ToString)
            finally
            {
            }
        }

        // Store Debugs in Stack / HashTable
        // Add Error To Stack
        public void AddErrorToStack(Exception ex)
        {
            _myStack.Push(ex.Message);
        }

        // Clear the Stack.
        private void ClearStack()
        {
            int cntStack;
            cntStack = _myStack.Count;
            while (cntStack > 0)
            {
                _myStack.Pop();
                cntStack--;
            }
        }

        private string GetFileName()
        {
            string curDate;
            int intHour;
            string curDay = DateTime.Now.Day.ToString();
            string curMonth = DateTime.Now.Month.ToString();
            string curYear = DateTime.Now.Year.ToString();
            string fileName = "";
            try
            {
                if (curDay.Length == 1)
                    curDay = "0" + curDay;

                if (curMonth.Length == 1)
                    curMonth = "0" + curMonth;

                intHour = DateTime.Now.Hour;
                curDate = curDay + curMonth + curYear;

                if (!FileName.EndsWith(".txt"))
                {
                    string _fullFileName;
                    if (intHour >= 12)
                        _fullFileName = _fileName + "_" + curDate + "PM.txt";
                    else
                        _fullFileName = _fileName + "_" + curDate + "AM.txt";

                    fileName = _fullFileName;
                }
                else
                {
                    fileName = FileName;
                }
            }
            catch
            {
            }
            finally
            {
                curDate = null;
                intHour = default(int);
                curDay = null;
                curMonth = null;
                curYear = null;
            }
            return fileName;
        }

        public void RemoveFile(string FileName)
        {
            FileInfo fileInformation = new FileInfo(FileName);
            fileInformation.Delete();
            fileInformation = null;
        }

        private string GetUniqueFileName(string Path, string Root)
        {
            string uniqueFileName = "";
            string curDate;
            string curDay = DateTime.Now.Day.ToString();
            string curMonth = DateTime.Now.Month.ToString();
            string curYear = DateTime.Now.Year.ToString();
            int intHour, intMinute, intSecond;
            int intTotFiles = 0;
            string strTemp;

            try
            {
                if (curDay.Length == 1)
                    curDay = "0" + curDay;

                if (curMonth.Length == 1)
                    curMonth = "0" + curMonth;

                intHour = DateTime.Now.Hour;
                intMinute = DateTime.Now.Minute;
                intSecond = DateTime.Now.Second;

                curDate = curDay + curMonth + curYear;


                Path = Path.TrimEnd(@"\ ".ToCharArray());
                Root = Root.TrimEnd(".txt".ToCharArray());

                do
                {
                    strTemp = string.Format(@"{0}\{1}_{2:00}", Path, Root, intTotFiles);

                    if (intHour >= 12 & (intMinute > 0 | intSecond > 0))
                        strTemp = strTemp + curDate + "PM.txt";
                    else
                        strTemp = strTemp + curDate + "AM.txt";
                    intTotFiles += 1;
                }
                while (File.Exists(strTemp) & intTotFiles < 100);

                uniqueFileName = strTemp;
            }
            catch
            {

            }
            finally
            {
                curDate = null;
                intHour = default(int);
                curDay = null;
                curMonth = null;
                curYear = null;
                strTemp = null;
            }
            return uniqueFileName;
        }

        private bool DriveExists(string _fullFileName)
        {
            // AT PRESENT THIS FUNCTION IS NOT USED 
            // The Function Returns True if the Drive specified in _fullFileName Exists. 
            _fullFileName = _fullFileName.Substring(0, 3);
            string[] strDrives;
            strDrives = Directory.GetLogicalDrives();
            System.Collections.IEnumerator en;
            en = strDrives.GetEnumerator();
            while (en.MoveNext())
            {
                if (System.Convert.ToString(en.Current) == _fullFileName.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        ~ErrorHandler()
        {
            if (_readerWriterLock.IsWriterLockHeld)
            {
                _readerWriterLock.ReleaseLock();
                _readerWriterLock = null;
            }
            _myStack.Clear();
            _myStack = null;
            objFileInfo = null;
            _errStr = null;
        }
    }

    public class FileStreamWriter : StreamWriter
    {
        //private bool _isDisposed;

        public FileStreamWriter(FileStream stream) : base(stream)
        {
        }

        public new void Dispose()
        {

        }

        ~FileStreamWriter()
        {
            //if (!_isDisposed)
            //{
            //    // Flush()
            //    Close();
            //    if (!TextWriter.CoreNewLine == null)
            //    {
            //        Array.Clear(TextWriter.CoreNewLine, 0, TextWriter.CoreNewLine.Length);
            //        this.CoreNewLine = null;
            //    }
            //}

            //base.Finalize();
        }
    }
}
