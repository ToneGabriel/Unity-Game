using System;

// Use this attribute on methods that are used as transition conditions (Obsolete)
[AttributeUsage(AttributeTargets.Method)]
public class ConditionAttribute : Attribute { }