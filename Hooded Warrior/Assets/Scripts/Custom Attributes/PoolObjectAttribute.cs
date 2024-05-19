using System;


// Use this attribute on components that are pooled
[AttributeUsage(AttributeTargets.Class)]
public class PoolObjectAttribute : Attribute { }