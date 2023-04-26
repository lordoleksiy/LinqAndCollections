using System;
using System.Collections.Generic;
using AutoMapper;
using CollectionsAndLinq.DAL.Entities;

namespace CollectionsAndLinq.BLL.Interfaces;

public interface IAutoMapper
{
    public Mapper ProjectMapper { get; }
    public Mapper TeamMapper { get; }
    public Mapper UserMapper { get; }
    public Mapper TaskMapper { get; }
    public Mapper TeamWithUsersMapper { get; }
    public TaskState StateMapper(string taskState);
}
