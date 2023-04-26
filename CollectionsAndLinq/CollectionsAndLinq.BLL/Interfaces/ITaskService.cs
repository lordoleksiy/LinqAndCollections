using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.DAL.Entities;
using Task = CollectionsAndLinq.DAL.Entities.Task;

namespace CollectionsAndLinq.BLL.Interfaces;

public interface ITaskService: IBaseService<TaskDTO, Task>
{}
