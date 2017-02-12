using System;
using System.Text; //ADD

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			//System.Text.UTF8Encoding -> System.Text.DBCSCodePageEncodingへ変換
            Console.WriteLine("日本語 文字CODE"); //ここが文字化けする。
            Console.WriteLine($"{nameof(Console.OutputEncoding)} = {Console.OutputEncoding}");
        }
    }
}
