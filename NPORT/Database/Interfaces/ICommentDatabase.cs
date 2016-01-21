using System.Collections.Generic;

namespace NPORT.Database.Interfaces
{
    interface ICommentDatabase<TComment, TKey, TNewsKey>
    {
        List<TComment> GetList();

        List<TComment> GetListForNews(TNewsKey id);

        TComment Find(TKey id);

        void Add(TComment comment);

        void Remove(TKey id);
    }
}
