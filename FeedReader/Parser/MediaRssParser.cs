﻿namespace CodeHollow.FeedReader.Parser;

using System.Xml.Linq;
using CodeHollow.FeedReader.Extensions;
using Feeds;

internal class MediaRssParser : AbstractXmlFeedParser
{
    public override BaseFeed Parse(string feedXml, XDocument feedDoc)
    {
        ArgumentNullException.ThrowIfNull(feedXml);
        ArgumentNullException.ThrowIfNull(feedDoc);

        // There has to be a root element, or else XDocument.Parse would have thrown.
        var rss = feedDoc.Root!;

        // Ensure there is a channel element. It doesn't make sense to try to parse an RSS feed
        //   without one.
        var channel = rss.GetElement("channel");
        if (channel is null)
        {
            throw new ArgumentException($"Document does not contain a 'channel' element. Unable to parse {nameof(MediaRssFeed)} from {nameof(feedXml)}: {feedXml}", nameof(feedDoc));
        }

        return new MediaRssFeed(feedXml, channel);
    }
}
