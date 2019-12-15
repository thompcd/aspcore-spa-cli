using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TestFramework
{
    public enum TestType
    {
        User,
        Automated,
        PowerCycling
    }

    public class LeakLog
    {
        public string TestName;
        public string TestCode;
        public string DateTime;
        public string SerialNumber;
        public string PartNumber;
        public string ModelNumber;
        public string GUID;
        public bool WriteToDatabase = true;

        public string Result;
        public string TestValue;
        public string UpperLimit;
        public string LowerLimit;
        public string OtherData1;

    }

    public class Test
    {
        #region Properties / Fields

        private volatile Dictionary<string, string> _parameters = new Dictionary<string, string>();

        private List<Test> _subTests = null;

        public Exception Exception;

        private bool _testSelected = false;
        public bool TestSelected
        {
            get { return _testSelected; }
            set
            {
                bool notify = _testSelected != value;
                _testSelected = value;
            }
        }

        private string _testName;
        public string TestName
        {
            get { return _testName; }
            set
            {
                bool notify = _testName != value;
                _testName = value;
            }
        
        }

        private string _lowerLimit;
        public string LowerLimit
        {
            get { return _lowerLimit; }
            set
            {
                bool notify = _lowerLimit != value;
                 _lowerLimit = value;
            }
        }

        private string _testValue;
        public string TestValue
        {
            get { return _testValue; }
            set
            {
                bool notify = _testValue != value;
                _testValue = value;
            }
    }

        private string _upperLimit;
        public string UpperLimit
        {
            get { return _upperLimit; }
            set
            {
                bool notify = _upperLimit != value;
                _upperLimit = value;
            }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                bool notify = _result != value;
                _result = value;
            }
        }

        private string _testStatus;
        public string TestStatus
        {
            get { return _testStatus; }
            set
            {
                bool notify = _testStatus != value;
                _testStatus = value;
            }
        }

        public string TestClass;
        public string TestCode;
        public string ExpectedValue;
        public uint SetupPGN;
        public uint SendPGN;
        public uint ReceivePGN;
        public int ResponseNumBytes;
        public byte SendByte1;
        public byte SendByte2;
        public byte SendByte3;
        public byte SendByte4;
        public byte SendByte5;
        public byte SendByte6;
        public byte SendByte7;
        public byte SendByte8;
        public bool OldTestHeadUsed;
        public TestType TypeOfTest;
        public string DateTime;
        public string MacAddress;
        public string SerialNumber;
        public string PartNumber;
        public string ModelNumber;
        public string GUID;
        public string OtherData1;
        public string OtherData2;
        public int PollWait;
        public string Phase;
        public bool WriteToDatabase = true;
        
        #endregion

        public Test(){}


        public Test(string testName, string lowerLimit, string upperLimit, string testCode, uint sendPGN = 0, uint receivePGN = 0, string testClass = "")
        {
            TestName = testName;
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            TestCode = testCode;
            SendPGN = sendPGN;
            ReceivePGN = receivePGN;

            if(testClass == "")
            {
                TestClass = testCode;
            }
        }

        public override string ToString()
        {
            //_dispatcher
            //_parameters
            //_subTests
            //Parameters
            //Exception
            //LowerLimit
            //UpperLimit
            //TestStatus
            //ExpectedValue;
            //SetupPGN;
            //SendPGN;
            //ReceivePGN;
            //ResponseNumBytes;
            //SendByte1;
            //SendByte2;
            //SendByte3;
            //SendByte4;
            //SendByte5;
            //SendByte6;
            //SendByte7;
            //SendByte8;
            //OldTestHeadUsed;
            //TypeOfTest;
            //MacAddress;
            //OtherData1;
            //OtherData2;
            //PollWait;
            //Phase;

            return $"Serial Number: {SerialNumber}, " + 
                   $"Part Number: {PartNumber}, " +
                   $"Model Number: {ModelNumber}, " +
                   $"GUID: {GUID}, " +
                   $"Date/Time: {DateTime}, " +
                   $"Test Name: {TestName}, " +
                   $"Test Code: {TestCode}, " +
                   $"Test Value: {TestValue}, " +
                   $"Result: {Result}";
        }
    }
}
