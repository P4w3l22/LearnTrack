﻿
using LearnTrack.MVVM.Models;

namespace LearnTrack.Services;

public interface IStatisticsService
{
	List<Subject> GetSubjectsTopicsCount();
	List<bool> GetLast7DaysActivity();
	List<string> GetLatesSubjects();
}