using System;
using System.Linq;
using System.Text;
using System.Text.Json;

enum SubjectType
{
    Lecture,
    Practice
}

enum EvristicType
{
    MRV,
    Degree,
    LCV,
    Default
}

class BaseEntity
{
    public int Id { get; set; }
}

class Group : BaseEntity
{
    public string Name { get; set; }
    public int Count { get; set; }
    public int Subgroup { get; set; }
}

class Teacher : BaseEntity
{
    public string Name { get; set; }
    public List<int> SubjectIds { get; set; }
}

class Subject : BaseEntity
{
    public string Name { get; set; }
    public SubjectType SubjectType { get; set; }
}

class Room : BaseEntity
{
    public string Name { get; set; }
    public int Capacity { get; set; }
}

class TimeSlot
{
    public int Day { get; set; }
    public int Pair { get; set; }
}

class ScheduleRequirment : BaseEntity
{
    public int GroupId { get; set; }
    public int SubjectId { get; set; }
}

class ScheduleRequirmentDTO
{
    public int GroupId { get; set; }
    public int SubjectId { get; set; }
    public int Hours { get; set; }
}

class ScheduleSlot
{
    public int ScheduleRequirmentId { get; set; }
    public int TimeSlot { get; set; }
    public int RoomId { get; set; }
    public int TeacherId { get; set; }
}

class Program
{
    static List<Group> groups = new List<Group> {
            new Group { Id = 1, Name = "G1", Count = 30, Subgroup = 1 },
            new Group { Id = 2, Name = "G1", Count = 30, Subgroup = 2 },
            new Group { Id = 3, Name = "G2", Count = 25, Subgroup = 1 },
            new Group { Id = 4, Name = "G2", Count = 25, Subgroup = 2 },
            new Group { Id = 5, Name = "G3", Count = 35, Subgroup = 1 },
            new Group { Id = 6, Name = "G3", Count = 35, Subgroup = 2 },
        };

    static List<Subject> subjects = new List<Subject>
        {
            new Subject { Id = 1, Name = "S1", SubjectType = SubjectType.Lecture },
            new Subject { Id = 2, Name = "S2", SubjectType = SubjectType.Lecture },
            new Subject { Id = 3, Name = "S3", SubjectType = SubjectType.Lecture },
            new Subject { Id = 4, Name = "S1", SubjectType = SubjectType.Practice },
            new Subject { Id = 5, Name = "S2", SubjectType = SubjectType.Practice },
            new Subject { Id = 6, Name = "S3", SubjectType = SubjectType.Practice },
        };

    static List<Room> rooms = new List<Room>
        {
            new Room { Id = 1, Name = "R1", Capacity = 30 },
            new Room { Id = 2, Name = "R2", Capacity = 25 },
            new Room { Id = 3, Name = "R3", Capacity = 35 },
        };

    static List<Teacher> teachers = new List<Teacher>
        {
            new Teacher {
                Id = 1,
                Name = "T1",
                SubjectIds = new List<int> { 1, 4 }
            },
            new Teacher {
                Id = 2,
                Name = "T2",
                SubjectIds = new List<int> { 2, 5, 1, 4 }
            },
            new Teacher {
                Id = 3,
                Name = "T3",
                SubjectIds = new List<int> { 3, 6, 1, 4 }
            },
        };

    static List<int> timeSlots = Enumerable.Range(1, 20).ToList();

    static void Main()
    {
        using FileStream openStreamGroups = File.OpenRead("../../../groups.json");
        groups = JsonSerializer.Deserialize<List<Group>>(openStreamGroups);
        using FileStream openStreamRooms = File.OpenRead("../../../rooms.json");
        rooms = JsonSerializer.Deserialize<List<Room>>(openStreamRooms);
        using FileStream openStreamTeachers = File.OpenRead("../../../teachers.json");
        teachers = JsonSerializer.Deserialize<List<Teacher>>(openStreamTeachers);
        using FileStream openStreamSubjects = File.OpenRead("../../../subjects.json");
        subjects = JsonSerializer.Deserialize<List<Subject>>(openStreamSubjects);

        var scheduleRequirments = new List<ScheduleRequirment>
        {
            new ScheduleRequirment { Id = 1, GroupId = 1, SubjectId = 1 },
            new ScheduleRequirment { Id = 2, GroupId = 1, SubjectId = 1 },
            new ScheduleRequirment { Id = 3, GroupId = 1, SubjectId = 3 },
            new ScheduleRequirment { Id = 4, GroupId = 2, SubjectId = 1 },
            new ScheduleRequirment { Id = 5, GroupId = 2, SubjectId = 2 },
            new ScheduleRequirment { Id = 6, GroupId = 2, SubjectId = 3 },
            new ScheduleRequirment { Id = 7, GroupId = 3, SubjectId = 1 },
            new ScheduleRequirment { Id = 8, GroupId = 3, SubjectId = 2 },
            new ScheduleRequirment { Id = 9, GroupId = 3, SubjectId = 3 },
            new ScheduleRequirment { Id = 10, GroupId = 4, SubjectId = 1 },
            new ScheduleRequirment { Id = 11, GroupId = 4, SubjectId = 2 },
            new ScheduleRequirment { Id = 12, GroupId = 4, SubjectId = 3 },
            new ScheduleRequirment { Id = 13, GroupId = 5, SubjectId = 1 },
            new ScheduleRequirment { Id = 14, GroupId = 5, SubjectId = 2 },
            new ScheduleRequirment { Id = 15, GroupId = 5, SubjectId = 3 },
            new ScheduleRequirment { Id = 16, GroupId = 6, SubjectId = 1 },
            new ScheduleRequirment { Id = 17, GroupId = 6, SubjectId = 2 },
            new ScheduleRequirment { Id = 18, GroupId = 6, SubjectId = 3 },
            new ScheduleRequirment { Id = 19, GroupId = 1, SubjectId = 4 },
            new ScheduleRequirment { Id = 20, GroupId = 1, SubjectId = 5 },
            new ScheduleRequirment { Id = 21, GroupId = 1, SubjectId = 6 },
            new ScheduleRequirment { Id = 22, GroupId = 2, SubjectId = 4 },
            new ScheduleRequirment { Id = 23, GroupId = 2, SubjectId = 5 },
            new ScheduleRequirment { Id = 24, GroupId = 2, SubjectId = 6 },
            new ScheduleRequirment { Id = 25, GroupId = 3, SubjectId = 4 },
            new ScheduleRequirment { Id = 26, GroupId = 3, SubjectId = 5 },
            new ScheduleRequirment { Id = 27, GroupId = 3, SubjectId = 6 },
            new ScheduleRequirment { Id = 28, GroupId = 4, SubjectId = 4 },
            new ScheduleRequirment { Id = 28, GroupId = 4, SubjectId = 5 },
            new ScheduleRequirment { Id = 29, GroupId = 4, SubjectId = 6 },
            new ScheduleRequirment { Id = 30, GroupId = 5, SubjectId = 4 },
            new ScheduleRequirment { Id = 31, GroupId = 5, SubjectId = 5 },
            new ScheduleRequirment { Id = 32, GroupId = 5, SubjectId = 6 },
            new ScheduleRequirment { Id = 33, GroupId = 6, SubjectId = 4 },
            new ScheduleRequirment { Id = 34, GroupId = 6, SubjectId = 5 },
            new ScheduleRequirment { Id = 35, GroupId = 6, SubjectId = 6 },
        };

        scheduleRequirments = new List<ScheduleRequirment>();
        using FileStream openStreamscheduleRequirmentsDTOs = File.OpenRead("../../../scheduleRequirments.json");
        List<ScheduleRequirmentDTO> scheduleRequirmentDTOs = JsonSerializer.Deserialize<List<ScheduleRequirmentDTO>>(openStreamscheduleRequirmentsDTOs);
        int index = 1;
        foreach (var item in scheduleRequirmentDTOs)
        {
            int countPerWeek = (int)(item.Hours / 1.5 / 14);
            for (int i = 0; i < countPerWeek; i++)
            {
                scheduleRequirments.Add(new ScheduleRequirment { Id = index, GroupId = item.GroupId, SubjectId = item.SubjectId });
                index++;
            }
        }

        var schedule = new List<ScheduleSlot>();

        if (BacktrackingSearch(schedule, scheduleRequirments, EvristicType.MRV))
        {
            Console.WriteLine("Розклад складено:");
            foreach (var item in schedule.OrderBy(x => x.TimeSlot))
            {
                var scheduleRequirment = scheduleRequirments.FirstOrDefault(x => x.Id == item.ScheduleRequirmentId);
                var group = groups.FirstOrDefault(x => x.Id == scheduleRequirment.GroupId);
                var subject = subjects.FirstOrDefault(x => x.Id == scheduleRequirment.SubjectId);
                var teacher = teachers.FirstOrDefault(x => x.Id == item.TeacherId);
                var room = rooms.FirstOrDefault(x => x.Id == item.RoomId);
                if (subject.SubjectType == SubjectType.Practice)
                {
                    Console.WriteLine($"Time: {item.TimeSlot}; Day: {GetDay(item.TimeSlot)}; Pair: {GetPair(item.TimeSlot)}; Group: {group.Name}; Subgroup: {group.Subgroup}; Subject: {subject.Name}; Type: {subject.SubjectType}; Teacher: {teacher.Name}; Room: {room.Name}");
                }
                if (subject.SubjectType == SubjectType.Lecture)
                {
                    if (group.Subgroup == 1)
                    {
                        Console.WriteLine($"Time: {item.TimeSlot}; Day: {GetDay(item.TimeSlot)}; Pair: {item.TimeSlot}; Group: {group.Name}; Subject: {subject.Name}; Type: {subject.SubjectType}; Teacher: {teacher.Name}; Room: {room.Name}");
                    }
                }
            }
            Console.WriteLine($"Вартість шляху: {GetPathValue(schedule, scheduleRequirments)}");
            ExportToCsv(schedule, scheduleRequirments);
        }
        else
        {
            Console.WriteLine("Не вдалося знайти рішення.");
        }


    }

    static bool BacktrackingSearch(List<ScheduleSlot> schedule, List<ScheduleRequirment> scheduleRequirments, EvristicType evristicType)
    {
        if (schedule.Count == scheduleRequirments.Count)
        {
            return true;
        }

        ScheduleRequirment scheduleRequirment;
        if (evristicType == EvristicType.MRV)
        {
            scheduleRequirment = scheduleRequirments
                .OrderByDescending(x => schedule.Where(s => scheduleRequirments.FirstOrDefault(r => r.Id == s.ScheduleRequirmentId).GroupId == x.GroupId).Count())
                .ThenByDescending(x => schedule.Where(s => scheduleRequirments.FirstOrDefault(r => r.Id == s.ScheduleRequirmentId).SubjectId == x.SubjectId).Count())
                .FirstOrDefault(x => !schedule.Select(s => s.ScheduleRequirmentId).Contains(x.Id));
        }else if(evristicType == EvristicType.Degree)
        {
            scheduleRequirment = scheduleRequirments
                .OrderByDescending(x => scheduleRequirments.Where(s => !schedule.Select(c => c.ScheduleRequirmentId).Contains(x.Id) && s.GroupId == x.GroupId).Count())
                .ThenByDescending(x => scheduleRequirments.Where(s => !schedule.Select(c => c.ScheduleRequirmentId).Contains(x.Id) && s.SubjectId == x.SubjectId).Count())
                .FirstOrDefault(x => !schedule.Select(s => s.ScheduleRequirmentId).Contains(x.Id));
        }
        else
        {
            scheduleRequirment = scheduleRequirments.FirstOrDefault(x => !schedule.Select(s => s.ScheduleRequirmentId).Contains(x.Id));
        }

        if (scheduleRequirment is null)
        {
            return true;
        }

        foreach (var timeSlot in timeSlots.OrderByDescending(x => schedule.Where(s => s.TimeSlot == x).Count()))
        {
            foreach (var room in rooms.OrderByDescending(x => schedule.Where(s => s.RoomId == x.Id).Count()))
            {
                foreach (var teacher in teachers.OrderByDescending(x => schedule.Where(s => s.TeacherId == x.Id).Count()))
                {
                    if (subjects.FirstOrDefault(x => x.Id == scheduleRequirment.SubjectId).SubjectType == SubjectType.Lecture)
                    {
                        var scheduleRequirment2 = scheduleRequirments
                            .FirstOrDefault(
                            x => x.Id != scheduleRequirment.Id &&
                            x.SubjectId == scheduleRequirment.SubjectId &&
                            groups.FirstOrDefault(g => g.Id == x.GroupId).Name == groups.FirstOrDefault(g => g.Id == scheduleRequirment.GroupId).Name &&
                            groups.FirstOrDefault(g => g.Id == x.GroupId).Subgroup != groups.FirstOrDefault(g => g.Id == scheduleRequirment.GroupId).Subgroup &&
                            !schedule.Select(s => s.ScheduleRequirmentId).Contains(x.Id));
                        if (IsValid(schedule, scheduleRequirments, scheduleRequirment, timeSlot, room, teacher) && IsValid(schedule, scheduleRequirments, scheduleRequirment2, timeSlot, room, teacher))
                        {
                            var scheduleSlot = new ScheduleSlot { ScheduleRequirmentId = scheduleRequirment.Id, TimeSlot = timeSlot, RoomId = room.Id, TeacherId = teacher.Id };
                            var scheduleSlot2 = new ScheduleSlot { ScheduleRequirmentId = scheduleRequirment2.Id, TimeSlot = timeSlot, RoomId = room.Id, TeacherId = teacher.Id };
                            schedule.Add(scheduleSlot);
                            schedule.Add(scheduleSlot2);
                            if (BacktrackingSearch(schedule, scheduleRequirments, evristicType))
                            {
                                return true;
                            }
                            schedule.Remove(scheduleSlot);
                            schedule.Remove(scheduleSlot2);
                        }
                    }
                    else
                    {
                        if (IsValid(schedule, scheduleRequirments, scheduleRequirment, timeSlot, room, teacher))
                        {
                            var scheduleSlot = new ScheduleSlot { ScheduleRequirmentId = scheduleRequirment.Id, TimeSlot = timeSlot, RoomId = room.Id, TeacherId = teacher.Id };
                            schedule.Add(scheduleSlot);
                            if (BacktrackingSearch(schedule, scheduleRequirments, evristicType))
                            {
                                return true;
                            }
                            schedule.Remove(scheduleSlot);
                        }
                    }
                }
            }
        }
        return false;
    }


    static bool IsValid(List<ScheduleSlot> schedule, List<ScheduleRequirment> scheduleRequirments, ScheduleRequirment scheduleRequirment, int timeSlot, Room room, Teacher teacher )
    {
        var group = groups.FirstOrDefault(x => x.Id == scheduleRequirment.GroupId);
        var subject = subjects.FirstOrDefault(x => x.Id == scheduleRequirment.SubjectId);
        
        foreach (var item in schedule)
        {
            if (item.TimeSlot == timeSlot && item.RoomId == room.Id &&
                (scheduleRequirments.FirstOrDefault(x => x.Id == item.ScheduleRequirmentId)?.SubjectId != subject.Id || item.TeacherId != teacher.Id || !(subject.SubjectType == SubjectType.Lecture)))
            {
                return false;
            }
                

            if (item.TimeSlot == timeSlot && scheduleRequirments.FirstOrDefault(x => x.Id == item.ScheduleRequirmentId)?.GroupId == scheduleRequirment.GroupId)            {
                return false;
            }
                

            if (item.TimeSlot == timeSlot && item.TeacherId == teacher.Id &&
                (item.RoomId != room.Id || scheduleRequirments.FirstOrDefault(x => x.Id == item.ScheduleRequirmentId)?.SubjectId != subject.Id || !(subject.SubjectType == SubjectType.Lecture)) ||
                !teacher.SubjectIds.Contains(subject.Id))
            {
                return false;
            }
                
        }
        return true;
    }

    static int GetPathValue(List<ScheduleSlot> schedule, List<ScheduleRequirment> scheduleRequirments)
    {
        var pathValue = 0;
        foreach (var teacher in teachers)
        {
            var teacherLessons = schedule.Where(x => x.TeacherId == teacher.Id);
            foreach (var item in teacherLessons)
            {
                foreach (var item2 in teacherLessons.Where(x => GetDay(x.TimeSlot) == GetDay(item.TimeSlot)))
                {
                    if (item2.TimeSlot - item.TimeSlot > 1)
                    {
                        pathValue += item2.TimeSlot - item.TimeSlot - 1;
                    }
                }
            }
        }
        foreach (var group in groups)
        {
            var groupLessons = schedule.Where(x => scheduleRequirments.FirstOrDefault(s => s.Id == x.ScheduleRequirmentId).GroupId == group.Id);
            foreach (var item in groupLessons)
            {
                foreach (var item2 in groupLessons.Where(x => GetDay(x.TimeSlot) == GetDay(item.TimeSlot)))
                {
                    if (item2.TimeSlot - item.TimeSlot > 1)
                    {
                        pathValue += item2.TimeSlot - item.TimeSlot - 1;
                    }
                }
            }
        }

        var roomLessons = schedule.GroupBy(x => x.RoomId).ToList();
        foreach (var item in roomLessons)
        {
            var studentsCount = item.Select(x => groups.FirstOrDefault(g => scheduleRequirments.FirstOrDefault(s => s.Id == x.ScheduleRequirmentId).GroupId == g.Id).Count).Count();
            if (studentsCount > rooms.FirstOrDefault(x => x.Id == item.Key).Capacity)
            {
                pathValue += 1;
            }
        }

        return pathValue;
    }

    static int GetDay(int timeSlot)
    {
        int day = timeSlot % 4 == 0 ? (timeSlot / 4) : (timeSlot / 4) + 1;
        return day;
    }

    static int GetPair(int timeSlot)
    {
        int pair = timeSlot % 4 == 0 ? 4 : timeSlot % 4;
        return pair;
    }

    static void ExportToCsv(List<ScheduleSlot> schedule, List<ScheduleRequirment> scheduleRequirments)
    {
        var path = Environment.CurrentDirectory;
        var csvContent = new StringBuilder();
        csvContent.AppendLine("Day,Pair,Group,Subject,Type,Teacher,Room");
        using StreamWriter csvFile = new StreamWriter("../../../Schedule.csv");
        csvFile.WriteLine("Day,Pair,Group,Subject,Type,Teacher,Room");
        foreach (var item in schedule.OrderBy(x => x.TimeSlot))
        {
            var scheduleRequirment = scheduleRequirments.FirstOrDefault(x => x.Id == item.ScheduleRequirmentId);
            var group = groups.FirstOrDefault(x => x.Id == scheduleRequirment.GroupId);
            var subject = subjects.FirstOrDefault(x => x.Id == scheduleRequirment.SubjectId);
            var teacher = teachers.FirstOrDefault(x => x.Id == item.TeacherId);
            var room = rooms.FirstOrDefault(x => x.Id == item.RoomId);

            if (subject.SubjectType == SubjectType.Practice)
            {
                var scheduleSlot = $"{GetDay(item.TimeSlot)},{GetPair(item.TimeSlot)},{group.Name}-{group.Subgroup},{subject.Name},{subject.SubjectType},{teacher.Name},{room.Name}";
                csvContent.AppendLine(scheduleSlot);
                csvFile.WriteLine(scheduleSlot);
            }
            if (subject.SubjectType == SubjectType.Lecture)
            {
                if (group.Subgroup == 1)
                {
                    var scheduleSlot = $"{GetDay(item.TimeSlot)},{GetPair(item.TimeSlot)},{group.Name},{subject.Name},{subject.SubjectType},{teacher.Name},{room.Name}";
                    csvContent.AppendLine(scheduleSlot);
                    csvFile.WriteLine(scheduleSlot);
                }
            }
        }
    }
}