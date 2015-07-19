﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CServiceStack.ServiceHost;
using CServiceStack.Common.Types;

namespace ConsoleHelloWorldService
{
    /// <summary>
    /// SOA的实现（Route为Resful方式调用）
    /// </summary>
    public class HelloWorldServiceImp : IHelloWorldService
    {
        [Route("/sayHello")]
        public HelloResponseType Hello(HelloRequestType helloWorldIn)
        {
            return new HelloResponseType { Result = "Hello " + helloWorldIn.Name };
        }

        [Route("/check")]
        public CheckHealthResponseType CheckHealth(CheckHealthRequestType healthCheckIn)
        {
            return new CheckHealthResponseType();
        }


        #region 自定义方法的实现

        public GetLatestTimeResponseType GetLatestTime(GetLatestTimeType request)
        {
            string strDtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine("accept client request at " + strDtNow);
            return new GetLatestTimeResponseType { Result = strDtNow };
        }

        public AddNumberResponseType AddNumber(AddNumberRequestType request)
        {
            int result = request.num1 + request.num2;
            return new AddNumberResponseType { Result = result };
        }

        public AddPersonAgeResponseType AddPersonAge(AddPersonAgeRequestType request)
        {
            AddPersonAgeResponseType response = new AddPersonAgeResponseType();

            response.Person.Age = request.num1 + response.Person.Age;

            if (response.Person.Age > 60)
                response.IsPersonOlder = true;
            else
                response.IsPersonOlder = false;

            return response;
        }

        public GetAllPersonResponseType GetAllPerson(GetAllPersonRequestType request)
        {
            GetAllPersonResponseType response = new GetAllPersonResponseType();
            response.PersonList = new List<PersonModel>();
            for (int i = 0; i < request.num1; i++)
            {
                PersonModel p = new PersonModel();
                p.Name = "name" + i.ToString();
                p.Age = 20 + i;

                response.PersonList.Add(p);
            }
            response.Result = response.PersonList.Count;

            return response;
        }

        #endregion
    }
}
