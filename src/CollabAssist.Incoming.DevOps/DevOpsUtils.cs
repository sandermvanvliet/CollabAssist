﻿using System.Text.RegularExpressions;
using System.Web;
using CollabAssist.Incoming.DevOps.Models;

namespace CollabAssist.Incoming.DevOps
{
    public static class DevOpsUtils
    {
        private const string OrganizationRegex = @"(https:\/\/dev.azure.com/)(.*?)(\/.*)";

        public static string FormatPrUrl(DevOpsPullRequestNotification pr)
        {
            var projectUrl = pr.Resource.Repository.Project.Url;
            var regex = Regex.Match(projectUrl, OrganizationRegex);
            if (regex.Success)
            {
                var org = regex.Groups[2];
                var project = pr.Resource.Repository.Project.Name;
                var repository = pr.Resource.Repository.Name;
                var id = pr.Resource.PullRequestId;

                var url = $"https://dev.azure.com/{org}/{project}/_git/{repository}/pullrequest/{id}";
                return HttpUtility.UrlEncode(url);
            }

            return null;
        }

        public static string FormatPrUrl(string baseUrl, string project, string repository, string id)
        {
            return $"{baseUrl}/{project}/_git/{repository}/pullrequest/{id}";
        }
    }
}
